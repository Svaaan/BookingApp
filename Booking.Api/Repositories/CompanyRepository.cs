using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(CinemaDbContext context, ILogger<CompanyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            try
            {

                var createCompany = await _context.company.AddAsync(company);
                await _context.SaveChangesAsync();
                return createCompany.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating company");
                throw;
            }
        }

        public async Task<Company> GetCompanyById(int Id)
        {
            try
            {
                var getCompany = await _context.company.FindAsync(Id);
                if (getCompany == null)
                {
                    _logger.LogInformation($"Couldnt find a company in the database with the ID: {Id}.");
                }
                return getCompany;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<List<Company>> GetAllCompanyAsync()
        {
            try
            {
                var companyList = await _context.company.ToListAsync();
                if (companyList.Count == 0)
                {
                    _logger.LogInformation("No company found in the database.");
                }
                return companyList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve company from database");
                throw;
            }
        }

        public async Task<Company> DeleteCompanyByIdAsync(int Id)
        {
            try
            {
                var deleteCompany = await _context.company.FindAsync(Id);

                if (deleteCompany != null)
                {
                    CompanyValidator.ValidateCompany(deleteCompany);
                    _context.company.Remove(deleteCompany);
                    await _context.SaveChangesAsync();
                }
                return deleteCompany;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting company. CompanyId: {CompanyId}", Id);
                throw;
            }
        }
        public async Task<Company> UpdateCompanyById(int companyId, Company updateCompany)
        {
            try
            {
                var company = await _context.company.FindAsync(companyId);

                if (company == null)
                {
                    _logger.LogError($"No company found with the Id: {companyId}");
                    return null;
                }

                company.CompanyName = updateCompany.CompanyName;
                company.Email = updateCompany.Email;
                company.Employees = updateCompany.Employees;
                company.MovieTheatres = updateCompany.MovieTheatres;


                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating company. CompanyId: {companyId}", ex);
                throw;
            }
        }
    }
}