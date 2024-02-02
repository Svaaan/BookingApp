namespace Booking.Api.Entities.DTO
{
    public class ReservationDto
    {
        public int ShowId { get; set; }
        public string BookerEmail { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
    }
}
