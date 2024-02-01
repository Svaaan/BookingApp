using Booking.View.DTO;

namespace Booking.View.Request
{
    public interface IBookerRequest
    {
        Task <bool> RequestBooking(BookerDTO booker);
    }
}
