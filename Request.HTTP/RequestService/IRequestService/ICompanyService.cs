using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface ICompanyService
    {
        Task<bool> PostCompany(CompanyDTO company);
    }
}
