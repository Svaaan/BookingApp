using System.Text;
using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;

namespace Request.HTTP.RequestService
{
    public class SalonService : ISalonService
    {

        public async Task<bool> PostSalon(SalonDTO salon)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var endpoint = new Uri("https://localhost:44367/api/Salon");
                    var jsonContent = JsonConvert.SerializeObject(salon);
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

        public async Task<List<SalonDTO>> GetSalon()
        {
            HttpClient httpClient = new HttpClient();

            var getSalon = await httpClient.GetFromJsonAsync<List<SalonDTO>>("https://localhost:44367/api/Salon");

            return getSalon;
        }
        public async Task<bool> RemoveSalonById(int salonId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Salon/{salonId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing salon: {ex.Message}");
                return false;
            }
        }
        public async Task<SalonDTO> EditSalonById(SalonDTO salon)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Salon/{salon.ID}", salon);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SalonDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating salon. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating salon: {ex.Message}");
                return null;
            }
        }
    }
}
