using Booking.View.DTO.MovieTheatreDTO;

namespace Booking.View.Services.IServices
{
    public interface IBookerRequest
    {
        Task<bool> RequestBooking(BookerDTO booker);
    }
}
