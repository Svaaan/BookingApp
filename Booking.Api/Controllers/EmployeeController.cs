using Microsoft.AspNetCore.Mvc;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using System.Reflection;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        /// <param name="employee">The employee to create.</param>
        /// <returns>The employee employee.</returns>
        /// <response code="200">Returns the newly created employee.</response>
        /// <response code="400">If the employee is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] IncomingEmployeeDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                return BadRequest("Employee object is null.");
            }

            //hårdkodar en roll sålänge
            var employee = new Employee
            {
                Id = employeeDTO.Id,
                CompanyId = employeeDTO.CompanyId,
                Email = employeeDTO.Email,
                Name = employeeDTO.Name,
                LastName = employeeDTO.LastName,
                Password = employeeDTO.Password,
                Role = Enum.TryParse<EmployeeRole>(employeeDTO.Role, out var role) ? role : EmployeeRole.Employee,
                Salt = employeeDTO.Salt
            };
            var createEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
            return Ok(createEmployee);
        }
        /// <summary>
        /// Lists all employee.
        /// </summary>
        /// <returns>A list of employee.</returns>
        /// <response code="200">Returns the list of employee.</response>
        /// <response code="500">If an error occurs while retrieving the employee.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeDTO>>> ListAllEmployee()
        {
            var employeeList = await _employeeRepository.GetAllEmployeesAsync();
            if (employeeList == null)
            {
                return NotFound();
            }
            var employeeDtos = employeeList.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                Email = e.Email,
                LastName = e.LastName,
                Name = e.Name,
                Password = e.Password,
                Role = e.Role.ToString()
            }).ToList();
            return Ok(employeeDtos);
        }
 
        /// <summary>
        /// Delete a employee ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which employee that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployeeById(int id)
        {
            var deleteEmployee = await this._employeeRepository.DeleteEmployeeByIdAsync(id);
            if (deleteEmployee == null)
            {
                return NotFound();
            }
            return Ok(deleteEmployee);
        }

        /// <summary>
        /// Retrieve a specific employee from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Update a employee by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateEmployee"></param>
        /// <returns>Returns the updated employee.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, [FromBody] Employee updateEmployee)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateEmployee
                if (id != updateEmployee.Id)
                {
                    return BadRequest("Mismatched employee ID in the request.");
                }

                var updatedEmployee = await _employeeRepository.UpdateEmployeeById(id, updateEmployee);
                if (updatedEmployee == null)
                {
                    return NotFound();
                }

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating employee. employeeId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the employee.");
            }
        }
    }
}
