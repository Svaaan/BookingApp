using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Booking.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
