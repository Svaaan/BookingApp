using Newtonsoft.Json;
using Request.HTTP.DTO.MovieTheatreDTO;
using Request.HTTP.RequestService.IRequestService;
using System.Text;

namespace Request.HTTP.RequestService
{
    public class ReservationService : IReservationService
    {
        public async Task<bool> PostReservation(ReservationDTO reservation)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var endpoint = new Uri("https://localhost:44367/api/Reservation");
                    var jsonContent = JsonConvert.SerializeObject(reservation);
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

        public async Task<List<ReservationDTO>> GetReservation()
        {
            HttpClient httpClient = new HttpClient();

            var getReservation = await httpClient.GetFromJsonAsync<List<ReservationDTO>>("https://localhost:44367/api/Reservation");

            return getReservation;
        }
        public async Task<bool> RemoveReservationById(int reservationId)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:44367/api/Reservation/{reservationId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing reservation: {ex.Message}");
                return false;
            }
        }
        public async Task<ReservationDTO> EditReservationById(ReservationDTO reservationDTO)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"https://localhost:44367/api/Reservation/{reservationDTO.Id}", reservationDTO);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ReservationDTO>();
                }
                else
                {
                    Console.WriteLine($"Error updating reservation. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating reservation: {ex.Message}");
                return null;
            }
        }
    }
}
