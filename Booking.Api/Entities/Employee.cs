using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EmployeeRole Role { get; set; }
    }

    public enum EmployeeRole
    {
        Admin,
        Manager,
        Employee
    }
}
