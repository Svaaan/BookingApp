namespace Booking.Api.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public List <MovieTheatre> MovieTheatres { get; set; }
        public List <User> Users { get; set; }
    }
}
