using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Booking.Api.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
