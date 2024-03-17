namespace Booking.Api.Entities
{
    public class Show
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        public int SalonID { get; set; }
        public Salon Salon { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
