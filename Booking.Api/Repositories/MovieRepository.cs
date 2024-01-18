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

        public async Task<Movie> GetMovieById(int Id)
        {
            try
            {
                var getMovie = await _context.movies.FindAsync(Id);
                if (getMovie == null)
                {
                    _logger.LogInformation($"Couldnt find a movie in the database with the ID: {Id}.");
                }
                return getMovie;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
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
        public async Task<Movie> UpdateMovieById(int movieId, Movie updateMovie)
        {
            try
            {
                var movie = await _context.movies.FindAsync(movieId);
                if (movie == null)
                {
                    _logger.LogError($"No movie found with the Id: {movieId}");
                    return null;
                }
                movie.Title = updateMovie.Title;
                movie.Description = updateMovie.Description;
                movie.Director = updateMovie.Director;
                movie.Hours = updateMovie.Hours;
                movie.Minutes = updateMovie.Minutes;
                movie.ReleaseYear = updateMovie.ReleaseYear;
                movie.AgeRestriction = updateMovie.AgeRestriction;
                movie.MaxShows = updateMovie.MaxShows;

                await _context.SaveChangesAsync();
                return movie;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating movie. MovieId: {movieId}", ex);
                throw;
            }
        }
    }
}
