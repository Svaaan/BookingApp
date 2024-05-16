using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeById(int id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> DeleteEmployeeByIdAsync(int Id);
        Task<Employee> UpdateEmployeeById(int employeeId, Employee updateEmployee);
    }
}
