namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class EditShowDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int SalonId { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
    }
}
