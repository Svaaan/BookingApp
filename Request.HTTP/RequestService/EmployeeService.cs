//using Booking.Api.Entities.DTO;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;
using System.Net.Http;
using System.Net.Http.Json;

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
            return await _HttpClient.PostAsJsonAsync("api/employee", employee);
        }
        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            HttpClient httpClient = new HttpClient();

            var getEmployee = await httpClient.GetFromJsonAsync<List<EmployeeDTO>>("https://localhost:44367/api/Employee");

            return getEmployee;
        }
        public async Task<bool> RemoveEmployeeById(int employeeId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Booker/{employeeId}");
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
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Booker/{employeeDTO.Id}", employeeDTO);

                if (response.IsSuccessStatusCode)
                {
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
