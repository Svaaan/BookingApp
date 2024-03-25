namespace Booking.Api.Entities.DTO
{
    public class CreateReservationDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int BookedSeats { get; set; }
        public int BookerId { get; set; }
    }
}
