using System.Text;
using Newtonsoft.Json;


namespace Request.Http.DTO.MovieTheatreDTO
{
    public class BookerRequest : IBookerRequest
    {

        public async Task <bool> RequestBooking(BookerDTO booker)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:44367/api/Booker");
                    var jsonContent = JsonConvert.SerializeObject(booker);
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

