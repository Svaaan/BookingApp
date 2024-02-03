using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{

    public interface IBookerRepository
    {
        Task<Booker> CreateBookerAsync(Booker booker);
        Task<Booker> GetBookerById(int id);
        Task<List<Booker>> GetAllBookersAsync();
        Task<Booker> DeletebookerByIdAsync(int Id);
        Task<Booker> UpdateBookerById(int bookerId, Booker updateBooker);
    }
}

