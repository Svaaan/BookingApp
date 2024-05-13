using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;
using System.Text;

namespace Request.HTTP.RequestService
{
    public class CompanyService : ICompanyService
    {
        public async Task<bool> PostCompany(CompanyDTO company)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var endpoint = new Uri("https://localhost:44367/api/Company");
                    var jsonContent = JsonConvert.SerializeObject(company);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(endpoint, httpContent);
                    var result = await response.Content.ReadAsStringAsync();

                    return true;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return false;
            }
        }
    }
}
