using System.Text.Json.Serialization;

namespace Booking.Api.Entities.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
    }
}
