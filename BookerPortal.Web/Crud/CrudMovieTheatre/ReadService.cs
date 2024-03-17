using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Booking.Api.Entities; 
using System.Net.Http.Json;
using Booking.View.DTO.MovieTheatreDTO;
using Newtonsoft.Json;

namespace BookerPortal.Web.Crud.CrudMovieTheatre
{
    public class ReadService
    {

        private readonly HttpClient _httpClient;

        public ReadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookerDTO>> GetAllBookers()
        {
            try
            {
                var endpoint = new Uri("https://localhost:44367/api/Booker");
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                var jsonContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BookerDTO>>(jsonContent);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

    }
}
