using Microsoft.AspNetCore.Mvc;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using System.Reflection;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTheatreController : ControllerBase
    {
        private readonly IMovieTheatreRepository _movieTheatreRepository;
        private readonly ILogger<MovieTheatreController> _logger;

        public MovieTheatreController(IMovieTheatreRepository MovieTheatreRepository, ILogger<MovieTheatreController> logger)
        {
            _movieTheatreRepository = MovieTheatreRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new MovieTheatre.
        /// </summary>
        /// <param name="MovieTheatre">The user to create.</param>
        /// <returns>The User user.</returns>
        /// <response code="200">Returns the newly created user.</response>
        /// <response code="400">If the user is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieTheatre>> PostUser([FromBody] MovieTheatreDTO movieTheatreDTO)
        {
            if (movieTheatreDTO == null)
            {
                return BadRequest("MovieTheatre object is null.");
            }

            PropertyInfo[] properties = typeof(MovieTheatreDTO).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(movieTheatreDTO) == null)
                {
                    return BadRequest($"Property {property.Name} is null.");
                }
            }

            var movieTheatre = new MovieTheatre{
            Id = movieTheatreDTO.Id, CompanyId = movieTheatreDTO.CompanyId, Name = movieTheatreDTO.Name};

            var createMovieTheatre = await _movieTheatreRepository.CreateMovieTheatreAsync(movieTheatre);
            return Ok(createMovieTheatre);
        }
        /// <summary>
        /// Lists all MovieTheatre.
        /// </summary>
        /// <returns>A list of MovieTheatre.</returns>
        /// <response code="200">Returns the list of MovieTheatre.</response>
        /// <response code="500">If an error occurs while retrieving the MovieTheatre.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<MovieTheatre>>> ListAllMovieTheatres()
        {
            var movieTheatreList = await _movieTheatreRepository.GetAllMovieTheatresAsync();
            if (movieTheatreList == null)
            {
                return NotFound();
            }
            return Ok(movieTheatreList);
        }

        /// <summary>
        /// Delete a MovieTheatre ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which MovieTheatre that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieTheatre>> DeleteMovieTheatreById(int id)
        {
            var deleteMovieTheatre = await this._movieTheatreRepository.DeleteMovieTheatreByIdAsync(id);
            if (deleteMovieTheatre == null)
            {
                return NotFound();
            }
            return Ok(deleteMovieTheatre);
        }

        /// <summary>
        /// Retrieve a specific MovieTheatre from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MovieTheatre>> GetMovieTheatreById(int id)
        {
            var movieTheatre = await _movieTheatreRepository.GetMovieTheatreById(id);

            if (movieTheatre == null)
            {
                return NotFound();
            }

            return Ok(movieTheatre);
        }

        /// <summary>
        /// Update a MovieTheatre by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateMovieTheatre"></param>
        /// <returns>Returns the updated MovieTheatre.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(MovieTheatre), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MovieTheatre>> UpdateMovieTheatre(int id, [FromBody] MovieTheatre updateMovieTheatre)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateMovieTheatre
                if (id != updateMovieTheatre.Id)
                {
                    return BadRequest("Mismatched MovieTheatre ID in the request.");
                }

                var updatedMovieTheatre = await _movieTheatreRepository.UpdateMovieTheatreById(id, updateMovieTheatre);
                if (updatedMovieTheatre == null)
                {
                    return NotFound();
                }

                return Ok(updatedMovieTheatre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating MovieTheatre. MovieTheatreId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the MovieTheatre.");
            }
        }
    }
}
