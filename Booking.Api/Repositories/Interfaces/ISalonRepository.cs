using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface ISalonRepository
    {
        Task<Salon> CreateSalonAsync(Salon salon);
        Task<List<Salon>> GetSalonsAsync();
        //Task<Salon> UpdateSalonAsync(int Id, Salon salon);
        //Task DeleteSalonAsync(int Id);
    }
}
