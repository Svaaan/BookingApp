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
        private readonly ILogger<SalonRepository> _logger;

        public SalonRepository(CinemaDbContext context, ILogger<SalonRepository> logger)
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

        public async Task<Salon> UpdateSalonById(int salonId, Salon updateSalon)
        {
            try
            {
                var salon = await _context.salons.FindAsync(salonId);
                if (salon == null)
                {
                    _logger.LogError($"No salon found with the Id: {salonId}");
                    return null;
                }
                SalonValidator.ValidateSalon(updateSalon);
                salon.Name = updateSalon.Name;
                salon.AvailableSeats = updateSalon.AvailableSeats;
                salon.Status = updateSalon.Status;

                await _context.SaveChangesAsync();
                return salon;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating salon. SalonId: {salonId}", ex);
                throw;
            }
        }

        public async Task<Salon> DeleteSalonById(int Id)
        {
            try
            {
                var deleteSalon = await _context.salons.FindAsync(Id);

                if (deleteSalon != null)
                {
                    SalonValidator.ValidateSalon(deleteSalon);
                    _context.salons.Remove(deleteSalon);
                    await _context.SaveChangesAsync();
                }
                return deleteSalon;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting movie. MovieId: {MovieId}", Id);
                throw;
            }
        }
    }
}
