using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IMovieTheatreRepository
    {
        Task<MovieTheatre> CreateMovieTheatreAsync(MovieTheatre movieTheatre);
        Task<MovieTheatre> GetMovieTheatreById(int id);
        Task<List<MovieTheatre>> GetAllMovieTheatresAsync();
        Task<MovieTheatre> DeleteMovieTheatreByIdAsync(int Id);
        Task<MovieTheatre> UpdateMovieTheatreById(int movieTheatreId, MovieTheatre updateMovieTheatre);
    }
}
