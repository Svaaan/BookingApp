using Ardalis.GuardClauses;
using Booking.Api.Data;
using Booking.Api.Entities;

namespace Booking.Api.Repositories
{
    public class MovieRepository
    {
        private readonly CinemaDbContext _context;

        public MovieRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var createMovie = await _context.movies.AddAsync(movie);

            await _context.SaveChangesAsync();
            return createMovie.Entity;

        }
    }
}
