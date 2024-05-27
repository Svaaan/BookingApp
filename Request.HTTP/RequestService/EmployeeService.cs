using Booking.Api.Data;
using Booking.Api.Entities;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;


namespace Request.HTTP.RequestService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _HttpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
            _HttpClient.BaseAddress = new Uri("https://localhost:44367/");
        }

        public async Task<HttpResponseMessage> PostEmployee(EmployeeDTO employee)
        {
            (string hashedPassword, byte[] salt) = PasswordHashing.HashPassword(employee.Password);
            employee.Password = hashedPassword;
            employee.Salt = salt;

            return await _HttpClient.PostAsJsonAsync("api/employee", employee);
        }
        public async Task<List<EmployeeDTO>> GetEmployee()
        {

            var getEmployee = await _HttpClient.GetFromJsonAsync<List<NoSaltEmployeeDTO>>("api/Employee");
            if(getEmployee == null)
            {
                return new List<EmployeeDTO>();
            }
            //FÖR VARJE EMPLOYEE HÄMTA DENS COMPANY VIA API/COMPANY/EMPLOYEE.COMPANYID
            List<EmployeeDTO> employees = getEmployee.Select(e => new EmployeeDTO()
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                Email = e.Email,
                LastName = e.LastName,
                Name = e.Name,
                Password = e.Password,
                Role = e.Role
            }).ToList();
            foreach(var employee in employees)
            {
                
                employee.Company = await _HttpClient.GetFromJsonAsync<CompanyDTO>($"api/Company/{employee.CompanyId}");
            }
            return employees;
        }
        public async Task<bool> RemoveEmployeeById(int employeeId)
        {
            try
            {
                HttpResponseMessage response = await _HttpClient.DeleteAsync($"api/Employee/{employeeId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing employee: {ex.Message}");
                return false;
            }
        }
        public async Task<EmployeeDTO> EditEmployeeById(EmployeeDTO employeeDTO)
        {
            try
            {
                HttpResponseMessage response = await _HttpClient.PutAsJsonAsync($"api/Employee/{employeeDTO.Id}", employeeDTO);

                if (response.IsSuccessStatusCode)
                {
                    (string hashedPassword, byte[] salt) = PasswordHashing.HashPassword(employeeDTO.Password);
                    employeeDTO.Password = hashedPassword;
                    employeeDTO.Salt = salt;

                    return await response.Content.ReadFromJsonAsync<EmployeeDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating employee. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
                return null;
            }
        }
    }
}
