using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string HashedPassword { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Manager,
        User
    }
}
