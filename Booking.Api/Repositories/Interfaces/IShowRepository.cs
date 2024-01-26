using Booking.Api.Entities;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IShowRepository
    {
        Task<Show> CreateShow(CreateShowDto showDto);
        Task<bool> IsShowTimeSlotAvailable(DateTime startTime, DateTime endTime, int salonId);
    }
}
