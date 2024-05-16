using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IEmployeeService
    {
        Task<HttpResponseMessage> PostEmployee(EmployeeDTO employee);
        Task<List<EmployeeDTO>> GetEmployee();
        Task<bool> RemoveEmployeeById(int employeeId);
        Task<EmployeeDTO> EditEmployeeById(EmployeeDTO employee);
    }
}
