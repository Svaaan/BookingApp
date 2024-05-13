using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;

namespace Request.HTTP.RequestService
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _HttpClient;
        public CompanyService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
            _HttpClient.BaseAddress = new Uri("https://localhost:44367/");
        }
        public async Task<HttpResponseMessage> PostCompany(CompanyDTO company)
        {
            return await _HttpClient.PostAsJsonAsync("/api/Company", company);
        }
    }
}
