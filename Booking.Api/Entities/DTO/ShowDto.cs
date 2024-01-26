namespace Booking.Api.Entities.DTO
{
    public class ShowDto
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
