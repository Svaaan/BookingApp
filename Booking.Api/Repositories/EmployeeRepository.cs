using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(CinemaDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeById(int Id)
        {
            try
            {
                var getEmployee = await _context.employees.Include(u => u.Company).Where(u => u.Id == Id).FirstAsync();
                if (getEmployee == null)
                {
                    _logger.LogInformation($"Couldnt find a employee in the database with the ID: {Id}.");
                }
                return getEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {

                var employeeList = await _context.employees.Include(u => u.Company).ToListAsync();
                if (employeeList.Count == 0)
                {
                    _logger.LogInformation("No employees found in the database.");
                }
                return employeeList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve employees from database");
                throw;
            }
        }

        public async Task<Employee> DeleteEmployeeByIdAsync(int Id)
        {
            try
            {
                var deleteEmployee = await _context.employees.FindAsync(Id);

                if (deleteEmployee != null)
                {
                    EmployeeValidator.ValidateEmployee(deleteEmployee);
                    _context.employees.Remove(deleteEmployee);
                    await _context.SaveChangesAsync();
                }
                return deleteEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee. EmployeeId: {EmployeeId}", Id);
                throw;
            }
        }
        public async Task<Employee> UpdateEmployeeById(int employeeId, Employee updateEmployee)
        {
            try
            {
                var employee = await _context.employees.FindAsync(employeeId);

                if (employee == null)
                {
                    _logger.LogError($"No employee found with the Id: {employeeId}");
                    return null;
                }

                employee.Name = updateEmployee.Name;
                employee.LastName = updateEmployee.LastName;
                employee.Email = updateEmployee.Email;
                employee.Password = updateEmployee.Password;
                employee.CompanyId = updateEmployee.CompanyId;

                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating employee. employeeId: {employeeId}", ex);
                throw;
            }
        }
    }
}