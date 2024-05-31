using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Booking.Api.Repositories;
using System.Reflection;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyRepository companyRepository, ILogger<CompanyController> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new Company.
        /// </summary>
        /// <param name="companyDTO">The company to create.</param>
        /// <returns>The Company company.</returns>
        /// <response code="200">Returns the newly created company.</response>
        /// <response code="400">If the company is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Company>> PostCompany([FromBody] CompanyDTO companyDTO)
        {
            if (companyDTO == null)
            {
                return BadRequest("Company object is null.");
            }

            PropertyInfo[] properties = typeof(CompanyDTO).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(companyDTO) == null)
                {
                    return BadRequest($"Property {property.Name} is null.");
                }
            }

            var company = new Company { Id = companyDTO.Id, CompanyName = companyDTO.CompanyName, Email = companyDTO.Email, Adress = companyDTO.Adress, Country = companyDTO.Country };

            var createCompany = await _companyRepository.CreateCompanyAsync(company);
            return Ok(createCompany);
        }
        /// <summary>
        /// Lists all company.
        /// </summary>
        /// <returns>A list of company.</returns>
        /// <response code="200">Returns the list of company.</response>
        /// <response code="500">If an error occurs while retrieving the company.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Company>>> ListAllCompany()
        {
            var companyList = await _companyRepository.GetAllCompanyAsync();
            if (companyList == null)
            {
                return NotFound();
            }
            return Ok(companyList);
        }

        /// <summary>
        /// Delete a company ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which company that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> DeleteCompanyById(int id)
        {
            var deleteCompany = await this._companyRepository.DeleteCompanyByIdAsync(id);
            if (deleteCompany == null)
            {
                return NotFound();
            }
            return Ok(deleteCompany);
        }

        /// <summary>
        /// Retrieve a specific Company from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Company>> GetCompanyById(int id)
        {
            var company = await _companyRepository.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        /// <summary>
        /// Update a company by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCompany"></param>
        /// <returns>Returns the updated company.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Company>> UpdateCompany(int id, [FromBody] Company updateCompany)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateCompany
                if (id != updateCompany.Id)
                {
                    return BadRequest("Mismatched company ID in the request.");
                }

                var updatedCompany = await _companyRepository.UpdateCompanyById(id, updateCompany);
                if (updatedCompany == null)
                {
                    return NotFound();
                }

                return Ok(updatedCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating company. CompanyId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the company.");
            }
        }
    }
}
