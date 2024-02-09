using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<ShowRepository> _logger;

        public ShowRepository(CinemaDbContext context, ILogger<ShowRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Show> CreateShow(ShowUpsertDto showDto)
        {
            if (await IsShowTimeSlotAvailable(showDto.StartTime, showDto.EndTime, showDto.SalonId))
            {
                var movie = _context.movies.FirstOrDefault(m => m.ID == showDto.MovieId);
                var salon = _context.salons.FirstOrDefault(s => s.ID == showDto.MovieId);

                if (movie != null && movie.MaxShows > 0 && await _context.shows.CountAsync(s => s.MovieID == showDto.MovieId) >= movie.MaxShows)
                {
                    throw new Exception("Maximum allowed shows for this movie has been reached.");
                }

                var show = new Show
                {
                    MovieID = showDto.MovieId,
                    Movie = movie,
                    SalonID = showDto.SalonId,
                    Salon = salon,
                    StartTime = showDto.StartTime,
                    EndTime = showDto.EndTime,

                    AvailableSeats = _context.salons.FirstOrDefault(s => s.ID == showDto.SalonId)?.AvailableSeats ?? 0
                };

                ShowValidator.ValidateShow(show);

                _context.shows.Add(show);
                await _context.SaveChangesAsync();
                return show;
            }
            else
            {
                throw new Exception("The show time slot has already been booked.");
            }
        }

        public async Task<bool> IsShowTimeSlotAvailable(DateTime startTime, DateTime endTime, int salonId)
        {
            bool isAvailable = await _context.shows
                .Where(show => show.SalonID == salonId && !(show.EndTime <= startTime || show.StartTime >= endTime))
                .AnyAsync();
            return !isAvailable;
        }

        public async Task<Show> UpdateShow(int showId, ShowUpsertDto showDto)
        {
            var show = await _context.shows
                .Include(s => s.Movie)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(s => s.ID == showId);

            if (show == null)
            {
                throw new Exception("Show not found");
            }

            try
            {
                ShowUpsertDtoValidator.ValidateShowUpsertDto(showDto);

                var movie = await _context.movies.FirstOrDefaultAsync(m => m.ID == showDto.MovieId);
                var salon = await _context.salons.FirstOrDefaultAsync(s => s.ID == showDto.SalonId);

                _context.Entry(show).Reference(s => s.Movie).Load();
                _context.Entry(show).Reference(s => s.Salon).Load();

                show.MovieID = showDto.MovieId;
                show.Movie = movie;

                show.SalonID = showDto.SalonId;
                show.Salon = salon;
                show.StartTime = showDto.StartTime;
                show.EndTime = showDto.EndTime;

                // Update AvailableSeats based on the changes in Salon
                show.AvailableSeats = salon?.AvailableSeats ?? 0;

                await _context.SaveChangesAsync();
                return show;
            }
            catch (ShowValidationException ex)
            {
                _logger.LogError(ex, "Show validation failed during update.");
                throw new Exception($"Validation failed: {ex.Message}");
            }
        }

        public async Task<Show> DeleteShowById(int Id)
        {
            var deleteShow = await _context.shows.FindAsync(Id);
            if (deleteShow != null)
            {
                _context.shows.Remove(deleteShow);
                await _context.SaveChangesAsync();
            }

            return deleteShow;
        }

        public async Task<ShowDetailsDto> GetShowById(int Id)
        {
            Show show = await _context.shows
                .Include(s => s.Movie)
                .Include(s => s.Salon)
                .FirstOrDefaultAsync(s => s.ID == Id);

            if (show == null)
            {
                return null;
            }

            ShowDetailsDto showDetailsDto = new ShowDetailsDto
            {
                ShowId = show.ID,
                MovieId = show.Movie.ID,
                MovieTitle = show.Movie.Title,
                MovieDescription = show.Movie.Description,
                MovieDirector = show.Movie.Director,
                Hours = show.Movie.Hours,
                minutes = show.Movie.Minutes,
                ReleaseYear = show.Movie.ReleaseYear,
                AgeRestriction = show.Movie.AgeRestriction,
                SalonId = show.Salon.ID,
                SalonName = show.Salon.Name,
                AvailableSeats = show.Salon.AvailableSeats,
                StartTime = show.StartTime,
                EndTime = show.EndTime,
                Genre = show.Movie.Genre,
                Language = show.Movie.Language,
                Subtitle = show.Movie.Subtitle,
            };
            return showDetailsDto;
        }

        public async Task<List<Schedule>> GetShowsByDateAndHours()
        {
            DateTime currentDate = DateTime.Now;

            List<Show> shows = await _context.shows
                .Include(s => s.Movie)
                .Include(s => s.Salon)
                .ToListAsync();

            var currentShows = shows.Where(s => s.StartTime <= currentDate && s.EndTime >= currentDate).ToList();
            var upcomingShows = shows.Where(s => s.StartTime > currentDate).ToList();

            var currentShowGroup = currentShows.GroupBy(s => s.StartTime.Date)
                .Select(g => new Schedule
                {
                    Date = g.Key,
                    Shows = g.Select(s => new ShowDto 
                    {
                        ShowId = s.ID,
                        MovieId = s.MovieID,
                        MovieTitle = s.Movie.Title,
                        SalonId = s.Salon.ID,
                        SalonName = s.Salon.Name,
                        AvailableSeats = s.AvailableSeats,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                    }).ToList(),
                })
                .OrderBy(g => g.Date)
                .ToList();

            var upcomingShowGroup = upcomingShows.GroupBy(s => s.StartTime.Date)
                .Select(g => new Schedule
                {
                    Date = g.Key,
                    Shows = g.Select(s => new ShowDto
                    {
                        ShowId = s.ID,
                        MovieId = s.MovieID,
                        MovieTitle = s.Movie.Title,
                        SalonId = s.Salon.ID,
                        SalonName = s.Salon.Name,
                        AvailableSeats = s.AvailableSeats,
                        StartTime = s.StartTime,
                        EndTime = s.EndTime,
                    }).ToList(),
                })
                .OrderBy(g => g.Date)
                .ToList();

            var showGroups = new List<Schedule>();
            showGroups.AddRange(currentShowGroup);
            showGroups.AddRange(upcomingShowGroup);
            return showGroups;
        }
    }
}
