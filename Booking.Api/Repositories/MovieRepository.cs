using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public MovieRepository(CinemaDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Movie> CreateMovieAsync(Movie movie)
        {
            try
            {
                MovieValidator.ValidateMovie(movie);

                var createMovie = await _context.movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return createMovie.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating movie. MovieId: {MovieId}, Title: {Title}", movie.ID, movie.Title);
                throw;
            }
        }
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            try
            {
                var movieList = await _context.movies.ToListAsync();
                if (movieList.Count == 0)
                {
                    // You might log a message indicating that there are no movies in the database.
                    _logger.LogInformation("No movies found in the database.");
                }
                return movieList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve movies from database");
                throw;
            }
        }

        public async Task<Movie> DeleteMovieByIdAsync(int Id)
        {
            try
            {
                var deleteMovie = await _context.movies.FindAsync(Id);

                if (deleteMovie != null)
                {
                    MovieValidator.ValidateMovie(deleteMovie);
                    _context.movies.Remove(deleteMovie);
                    await _context.SaveChangesAsync();
                }
                return deleteMovie;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting movie. MovieId: {MovieId}", Id);
                throw;
            }
        }
    }
}
