using Booking.Api.Entities.DTO;
using Request.HTTP.RequestService.IRequestService;
using System.Net.Http;
using System.Net.Http.Json;

namespace Request.HTTP.RequestService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _HttpClient;

        public UserService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
            _HttpClient.BaseAddress = new Uri("https://localhost:44367/");
        }

        public async Task<HttpResponseMessage> PostUser(UserDTO user)
        {
            return await _HttpClient.PostAsJsonAsync("api/user", user);
        }
    }
}
