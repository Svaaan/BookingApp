using Booking.Api.Entities.DTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IUserService
    {
        Task<HttpResponseMessage> PostUser(UserDTO user);
    }
}
