namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
        public int BookerId { get; set; }
    }
}