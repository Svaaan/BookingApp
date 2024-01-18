using System;
using Booking.Api.Entities;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie>CreateMovieAsync(Movie movie);
        Task<Movie> GetMovieById(int id);
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> DeleteMovieByIdAsync(int Id);
        Task<Movie> UpdateMovieById(int movieId, Movie updateMovie);
    }
}
