namespace Booking.Api.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public List<MovieTheatre> MovieTheatres = new();
        public List<Employee> Employees = new();
    }
}
