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

        public async Task<Show> CreateShow(CreateShowDto showDto)
        {
            if (await IsShowTimeSlotAvailable(showDto.StartTime, showDto.EndTime, showDto.SalonId))
            {
                var movie = _context.movies.FirstOrDefault(m => m.ID == showDto.MovieId);
                var salon = _context.salons.FirstOrDefault(s => s.ID == showDto.MovieId);

                if (movie != null && movie.MaxShows > 0 && await _context.shows.CountAsync(s => s.MovieID == showDto.MovieId) >= movie.MaxShows)
                {
                    throw new Exception("Maximum allowed show for this movie has been reached.");
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
    }
}
