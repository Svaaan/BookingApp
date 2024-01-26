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
        private readonly ILogger<MovieRepository> _logger;

        public ShowRepository(CinemaDbContext context, ILogger<MovieRepository> logger)
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

                    AvailableSeats = _context.salons.FirstOrDefault(s => s.ID == showDto.SalonId)?.NumberOfSeats ?? 0
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
            var show = await _context.shows.FirstOrDefaultAsync(s => s.ID == showId);
            if (show == null)
            {
                throw new Exception("Show not found");
            }

            try
            {
                ShowUpsertDtoValidator.ValidateShowUpsertDto(showDto);

                var movie = await _context.movies.FirstOrDefaultAsync(m => m.ID == showDto.MovieId);
                var salon = await _context.salons.FirstOrDefaultAsync(s => s.ID == showDto.SalonId);

                show.MovieID = showDto.MovieId;
                show.Movie = movie;

                show.SalonID = showDto.SalonId;
                show.Salon = salon;
                show.StartTime = showDto.StartTime;
                show.EndTime = showDto.EndTime;

                await _context.SaveChangesAsync();
                return show;

            }
            catch (ShowValidationException ex)
            {
                _logger.LogError(ex, "Show validation failed during update.");
                throw new Exception($"Validation failed: {ex.Message}");
            }
        }
    }
}
