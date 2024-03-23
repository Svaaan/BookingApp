namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ReservationDTO
    {
        public ShowDTO Show { get; set; }
        public string BookerEmail { get; set; }
        public int? BookedSeats { get; set; }
        public DateTime ReservationTime { get; set; } = DateTime.UtcNow;
    }
}