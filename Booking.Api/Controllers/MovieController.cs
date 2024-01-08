using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieRepository movieRepository, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new movie.
        /// </summary>
        /// <param name="movie">The movie to create.</param>
        /// <returns>The created movie.</returns>
        /// <response code="200">Returns the newly created movie.</response>
        /// <response code="400">If the movie is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> PostMovie([FromBody] Movie movie)
        {
            var createMovie = await this._movieRepository.CreateMovieAsync(movie);

            return Ok(createMovie);
        }

        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns>A list of movies.</returns>
        /// <response code="200">Returns the list of movies.</response>
        /// <response code="500">If an error occurs while retrieving the movies.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Movie>>> ListAllMovies()
        {
            var movieList = await _movieRepository.GetAllMoviesAsync();
            if (movieList == null)
            {
                return NotFound();
            }
            return Ok(movieList);
        }
    }
}
