using Booking.View.DTO;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Collections.Generic;

namespace Booking.View.Request
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

