using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;
using System.Text;

namespace Request.HTTP.RequestService
{
    public class ShowService : IShowService
    {
        public async Task<bool> PostShow(ShowDTO show)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var endpoint = new Uri("https://localhost:44367/api/Show");
                    var jsonContent = JsonConvert.SerializeObject(show);
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

        public async Task<List<ScheduleDTO>> GetShow()
        {
            HttpClient httpClient = new HttpClient();

            var getShow = await httpClient.GetFromJsonAsync<List<ScheduleDTO>>("https://localhost:44367/api/Show/schedule");

            return getShow;
        }
        public async Task<bool> RemoveShowById(int showId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Show/{showId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing show: {ex.Message}");
                return false;
            }
        }
        public async Task<ShowDTO> EditShowById(ShowDTO showDTO)
        {
            try
            {
                var editShowDTO = new EditShowDTO();
                editShowDTO.Id = showDTO.Id;
                editShowDTO.MovieId = showDTO.MovieId;
                editShowDTO.SalonId = showDTO.SalonId;
                editShowDTO.StartTime = showDTO.StartTime;
                editShowDTO.EndTime = showDTO.EndTime;

        HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Show/{editShowDTO.Id}", editShowDTO);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ShowDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating show. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating show: {ex.Message}");
                return null;
            }
        }
    }
}
