using Booking.Api.Entities.DTO;

namespace Booking.Api.Entities
{
    public class Schedule
    {
        public DateTime Date { get; set; }
        public List<ShowDto> Shows { get; set; }
    }
}
