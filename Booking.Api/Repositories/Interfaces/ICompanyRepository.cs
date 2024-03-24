using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> GetCompanyById(int id);
        Task<List<Company>> GetAllCompanyAsync();
        Task<Company> DeleteCompanyByIdAsync(int Id);
        Task<Company> UpdateCompanyById(int companyId, Company updateCompany);
    }
}
