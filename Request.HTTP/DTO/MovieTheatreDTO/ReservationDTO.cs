namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public int BookedSeats { get; set; }
        public int BookerId { get; set; }
        public string BookerEmail { get; set; }
    }
}