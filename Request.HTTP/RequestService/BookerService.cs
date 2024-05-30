using System.Text;
using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;


namespace Request.HTTP.RequestService
{
    public class BookerService : IBookerService
    {
        /// <summary>
        /// Recieve bookings in Booking.Web movietheatrebookingmodal
        /// </summary>
        /// <param name="booker"></param>
        /// <returns></returns>
        public async Task<BookerDTO> PostBooking(BookerDTO booker)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    booker.BookingNumber = GenerateBookingNumber();

                    var endpoint = new Uri("https://localhost:44367/api/Booker");
                    var jsonContent = JsonConvert.SerializeObject(booker);
                    var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(endpoint, httpContent);

                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var bookedDto = JsonConvert.DeserializeObject<BookerDTO>(responseContent);

                    return bookedDto;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return null;
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

        public async Task<BookerDTO> EditBookerById(BookerDTO bookerDTO)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Booker/{bookerDTO.Id}", bookerDTO);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<BookerDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating booker. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating booker: {ex.Message}");
                return null;
            }
        }

        private string GenerateBookingNumber()
        {
            Random random = new Random();
            string letters = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2)
                                          .Select(s => s[random.Next(s.Length)]).ToArray());
            string numbers = random.Next(10000, 99999).ToString();

            return letters + numbers;
        }
    }
}

