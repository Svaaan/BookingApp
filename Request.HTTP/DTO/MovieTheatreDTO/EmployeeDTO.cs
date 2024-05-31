namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int CompanyId { get; set; }
        public CompanyDTO Company = new();
        public string Role { get; set; }
    }
}
