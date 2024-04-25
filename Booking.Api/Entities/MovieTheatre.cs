using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class MovieTheatre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
