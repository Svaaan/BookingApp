using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface ISalonRepository
    {
        Task<Salon> CreateSalonAsync(Salon salon);
        Task<List<Salon>> GetSalonsAsync();
        Task<Salon> UpdateSalonById(int salonId, Salon updateSalon);
        Task<Salon> DeleteSalonById(int Id);
    }
}
