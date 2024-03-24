namespace Booking.Api.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public List<MovieTheatre> MovieTheatres = new();
        public List<User> Users = new();
    }
}
