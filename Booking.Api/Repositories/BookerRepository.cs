using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class BookerRepository : IBookerRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<BookerRepository> _logger;

        public BookerRepository(CinemaDbContext context, ILogger<BookerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Booker> CreateBookerAsync(Booker booker)
        {
            try
            {
                var createBooker = await _context.bookers.AddAsync(booker);
                await _context.SaveChangesAsync();
                return createBooker.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booker");
                throw;
            }
        }

        public async Task<Booker> GetBookerById(int Id)
        {
            try
            {
                var getBooker = await _context.bookers.FindAsync(Id);
                if (getBooker == null)
                {
                    _logger.LogInformation($"Couldnt find a booker in the database with the ID: {Id}.");
                }
                return getBooker;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<List<Booker>> GetAllBookersAsync()
        {
            try
            {
                var bookerList = await _context.bookers.ToListAsync();
                if (bookerList.Count == 0)
                {
                    _logger.LogInformation("No bookers found in the database.");
                }
                return bookerList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve bookers from database");
                throw;
            }
        }

        public async Task<Booker> DeletebookerByIdAsync(int Id)
        {
            try
            {
                var deleteBooker = await _context.bookers.FindAsync(Id);

                if (deleteBooker != null)
                {
                    BookerValidator.ValidateBooker(deleteBooker);
                    _context.bookers.Remove(deleteBooker);
                    await _context.SaveChangesAsync();
                }
                return deleteBooker;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booker. BookerId: {BookerId}", Id);
                throw;
            }
        }
        public async Task<Booker> UpdateBookerById(int bookerId, Booker updateBooker)
        {
            try
            {
                var booker = await _context.bookers.FindAsync(bookerId);

                if (booker == null)
                {
                    _logger.LogError($"No booker found with the Id: {bookerId}");
                    return null;
                }

                booker.Name = updateBooker.Name;
                booker.LastName = updateBooker.LastName;
                booker.PhoneNumber = updateBooker.PhoneNumber;
                booker.Email = updateBooker.Email;
           

                await _context.SaveChangesAsync();
                return booker;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating booker. BookerId: {bookerId}", ex);
                throw;
            }
        }
    }
}