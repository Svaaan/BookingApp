using Booking.Api.Entities;
using Booking.Api.ExceptionHandler;
using Booking.Api.Repositories;
using Booking.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonController : ControllerBase
    {
        private readonly ILogger<SalonController> _logger;
        private readonly ISalonRepository _salonRepository;

        public SalonController(ILogger<SalonController> logger, ISalonRepository salonRepository)
        {
            _logger = logger;
            _salonRepository = salonRepository;
        }

        /// <summary>
        /// Create a Salon for the Theatre
        /// </summary>
        /// <param name="salon"></param>
        /// <returns>Returns the created object with their values</returns>
        /// <response code="201">Returns the newly created Salon.</response>
        /// <response code="400">If the Salon values are not valid.</response>
        /// /// <response code="500">Processing failure.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Salon>> PostSalon([FromBody] Salon salon)
        {
            try
            {
                // Your repository logic here
                var createSalon = await _salonRepository.CreateSalonAsync(salon);
                return Ok(createSalon);
            }
            catch (CustomArgumentException customEx)
            {
                // Handle custom validation error
                return BadRequest(customEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Gets all salons.
        /// </summary>
        /// <returns>A list of salons.</returns>
        /// <response code="200">Returns the list of movies.</response>
        /// <response code="404">Returns not found if database is empty.</response>
        /// <response code="500">If an error occurs while retrieving the movies.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Movie>>> ListAllMovies()
        {
            var salonsList = await _salonRepository.GetSalonsAsync();
            if (salonsList == null)
            {
                return NotFound();
            }
            return Ok(salonsList);
        }
    }
}
