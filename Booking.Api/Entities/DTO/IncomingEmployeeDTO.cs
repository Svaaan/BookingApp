namespace Booking.Api.Entities.DTO
{
    public class IncomingEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
    }
}
