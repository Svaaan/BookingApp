namespace Booking.Api.Entities.DTO
{
    public class ReservationDto
    {
        public int ShowId { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
        public Booker Booker { get; set; }
    }
}
