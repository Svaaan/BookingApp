using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class BookerService : IBookerService
    {
        /// <summary>
        /// Recieve bookings in Booking.Web movietheatrebookingmodal
        /// </summary>
        /// <param name="booker"></param>
        /// <returns></returns>
        public async Task <bool> PostBooking(BookerDTO booker)
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

        public async Task<List<BookerDTO>> GetBooker()
        {
            HttpClient httpClient = new HttpClient();

            var getBooker = await httpClient.GetFromJsonAsync<List<BookerDTO>>("https://localhost:44367/api/Booker");

            return getBooker;
        }
        public async Task<bool> RemoveBookerById(int bookerId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Booker/{bookerId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing booker: {ex.Message}");
                return false;
            }
        }


    }

}

