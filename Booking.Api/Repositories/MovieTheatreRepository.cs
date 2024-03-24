using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;

namespace Booking.Api.Repositories
{
    public class MovieTheatreRepository : IMovieTheatreRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<MovieTheatreRepository> _logger;

        public MovieTheatreRepository(CinemaDbContext context, ILogger<MovieTheatreRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<MovieTheatre> CreateMovieTheatreAsync(MovieTheatre movieTheatre)
        {
            try
            {

                var createMovieTheatre = await _context.movieTheatres.AddAsync(movieTheatre);
                await _context.SaveChangesAsync();
                return createMovieTheatre.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating MovieTheatre");
                throw;
            }
        }
        public async Task<MovieTheatre> GetMovieTheatreById(int Id)
        {
            try
            {
                var getMovieTheatre = await _context.movieTheatres.Where(m => m.Id == Id).Include(m => m.Company).FirstAsync();
                if (getMovieTheatre == null)
                {
                    _logger.LogInformation($"Couldnt find a MovieTheatre in the database with the ID: {Id}.");
                }
                return getMovieTheatre;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<List<MovieTheatre>> GetAllMovieTheatresAsync()
        {
            try
            {
                var movieTheatreList = await _context.movieTheatres.Include(m => m.Company).ToListAsync();
                if (movieTheatreList.Count == 0)
                {
                    _logger.LogInformation("No MovieTheatre found in the database.");
                }
                return movieTheatreList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retrieve MovieTheatre from database");
                throw;
            }
        }

        public async Task<MovieTheatre> DeleteMovieTheatreByIdAsync(int Id)
        {
            try
            {
                var deleteMovieTheatre = await _context.movieTheatres.FindAsync(Id);

                if (deleteMovieTheatre != null)
                {
                    MovieTheatreValidator.ValidateMovieTheatre(deleteMovieTheatre);
                    _context.movieTheatres.Remove(deleteMovieTheatre);
                    await _context.SaveChangesAsync();
                }
                return deleteMovieTheatre;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting MovieTheatre. MovieTheatre: {MovieTheatre}", Id);
                throw;
            }
        }
        public async Task<MovieTheatre> UpdateMovieTheatreById(int movieTheatreId, MovieTheatre updateMovieTheatre)
        {
            try
            {
                var movieTheatre = await _context.movieTheatres.FindAsync(movieTheatreId);

                if (movieTheatre == null)
                {
                    _logger.LogError($"No MovieTheatre found with the Id: {movieTheatreId}");
                    return null;
                }

                movieTheatre.Name = updateMovieTheatre.Name;
                movieTheatre.CompanyId = updateMovieTheatre.CompanyId;
            


                await _context.SaveChangesAsync();
                return movieTheatre;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating MovieTheatre. MovieTheatreId: {movieTheatreId}", ex);
                throw;
            }
        }
    }
}