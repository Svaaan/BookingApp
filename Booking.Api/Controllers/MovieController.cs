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
        /// Create a new movie.
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

        /// <summary>
        /// Delete a movie ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which movie that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Movie>> DeleteMovieById(int id)
        {
            var deleteMovie = await this._movieRepository.DeleteMovieByIdAsync(id);
            if (deleteMovie == null)
            {
                return NotFound();
            }
            return Ok(deleteMovie);
        }

        /// <summary>
        /// Retrieve a specific Movie from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        /// <summary>
        /// Update a movie by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateMovie"></param>
        /// <returns>Returns the updated Movie.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, [FromBody] Movie updateMovie)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateMovie
                if (id != updateMovie.ID)
                {
                    return BadRequest("Mismatched movie ID in the request.");
                }

                var updatedMovie = await _movieRepository.UpdateMovieById(id, updateMovie);
                if (updatedMovie == null)
                {
                    return NotFound();
                }

                return Ok(updatedMovie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating movie. MovieId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the movie.");
            }
        }
    }
}
