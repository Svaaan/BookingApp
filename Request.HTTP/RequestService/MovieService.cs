using System.Text;
using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;


namespace Request.HTTP.RequestService
{
   
    public class MovieService : IMovieService
    {
     
        public async Task<bool> PostMovie(MovieDTO movie)
        {
            
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var endpoint = new Uri("https://localhost:44367/api/Movie");
                    var jsonContent = JsonConvert.SerializeObject(movie);
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

        public async Task<List<MovieDTO>> GetMovie()
        {
            HttpClient httpClient = new HttpClient();

            var getMovie = await httpClient.GetFromJsonAsync<List<MovieDTO>>("https://localhost:44367/api/Movie");

            return getMovie;
        }
        public async Task<bool> RemoveMovieById(int movieId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Movie/{movieId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing movie: {ex.Message}");
                return false;
            }
        }
        public async Task<MovieDTO> EditMovieById(MovieDTO movie)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Movie/{movie.ID}", movie);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MovieDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating movie. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating movie: {ex.Message}");
                return null;
            }
        }
    }
}
