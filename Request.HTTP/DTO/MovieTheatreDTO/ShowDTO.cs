namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ShowDTO
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public MovieDTO movie { get; set; }
        public int SalonID { get; set; }
        public SalonDTO Salon { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
