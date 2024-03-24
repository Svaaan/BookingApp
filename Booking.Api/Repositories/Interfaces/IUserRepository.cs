using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User> DeleteUserByIdAsync(int Id);
        Task<User> UpdateUserById(int userId, User updateUser);
    }
}
