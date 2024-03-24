using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(CinemaDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {

                var createUser = await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                return createUser.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }
        public async Task<User> GetUserById(int Id)
        {
            try
            {
                var getUser = await _context.users.FindAsync(Id);
                if (getUser == null)
                {
                    _logger.LogInformation($"Couldnt find a user in the database with the ID: {Id}.");
                }
                return getUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var userList = await _context.users.ToListAsync();
                if (userList.Count == 0)
                {
                    _logger.LogInformation("No users found in the database.");
                }
                return userList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve users from database");
                throw;
            }
        }

        public async Task<User> DeleteUserByIdAsync(int Id)
        {
            try
            {
                var deleteUser = await _context.users.FindAsync(Id);

                if (deleteUser != null)
                {
                    UserValidator.ValidateUser(deleteUser);
                    _context.users.Remove(deleteUser);
                    await _context.SaveChangesAsync();
                }
                return deleteUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user. UserId: {UserId}", Id);
                throw;
            }
        }
        public async Task<User> UpdateUserById(int userId, User updateUser)
        {
            try
            {
                var user = await _context.users.FindAsync(userId);

                if (user == null)
                {
                    _logger.LogError($"No user found with the Id: {userId}");
                    return null;
                }

                user.Name = updateUser.Name;
                user.LastName = updateUser.LastName;    
                user.Email = updateUser.Email;
                user.Password = updateUser.Password;
                user.CompanyId = updateUser.CompanyId;
              


                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user. UserId: {userId}", ex);
                throw;
            }
        }
    }
}