using Booking.Api.Entities;

namespace Request.Http.DTO.MovieTheatreDTO
{
    public class ReservationDto
    {
        public Show Show { get; set; }
        public string BookerEmail { get; set; }
        public int? BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
    }
}