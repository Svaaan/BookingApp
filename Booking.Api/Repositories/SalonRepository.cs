using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;


namespace Booking.Api.Repositories
{
    public class SalonRepository : ISalonRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public SalonRepository(CinemaDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Salon> CreateSalonAsync(Salon salon)
        {
            try
            {
                SalonValidator.ValidateSalon(salon);

                var createSalon = await _context.salons.AddAsync(salon);
                await _context.SaveChangesAsync();
                return createSalon.Entity;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating salon. Salon: Name: {Name}", salon.Name);

                throw;
            }
        }

        public async Task<List<Salon>> GetSalonsAsync()
        {
            try
            {
                var salonList = await _context.salons.ToListAsync();
                if (salonList.Count == 0)
                {
                    _logger.LogInformation("No salons found in the database.");
                }
                return salonList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve salons from database");
                throw;
            }
        }
    }
}
