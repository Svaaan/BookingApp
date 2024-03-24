using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Show Show { get; set; }
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
    }
}