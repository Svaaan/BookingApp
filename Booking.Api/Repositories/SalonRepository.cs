using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;


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
    }
}
