using Booking.Api.Entities.DTO;

namespace Booking.Api.Entities
{
    public class Schedule
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<ShowDto> Shows { get; set; }
    }
}
