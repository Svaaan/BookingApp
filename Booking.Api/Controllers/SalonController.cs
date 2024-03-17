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
        private readonly ISalonRepository _salonRepository;
        private readonly ILogger<SalonController> _logger;

        public SalonController(ILogger<SalonController> logger, ISalonRepository salonRepository)
        {
            _salonRepository = salonRepository;
            _logger = logger;
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
        public async Task<ActionResult<Salon>> CreateSalon([FromBody] Salon salon)
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
        public async Task<ActionResult<List<Salon>>> ListAllSalons()
        {
            var salonsList = await _salonRepository.GetSalonsAsync();
            if (salonsList == null)
            {
                return NotFound();
            }
            return Ok(salonsList);
        }

        /// <summary>
        /// Update a salon by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salon"></param>
        /// <returns>Returns the updated Salon.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Salon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Salon>> UpdateSalon(int id, [FromBody] Salon salon)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateMovie
                if (id != salon.ID)
                {
                    return BadRequest("Mismatched movie ID in the request.");
                }

                var updatedSalon = await _salonRepository.UpdateSalonById(id, salon);
                if (updatedSalon == null)
                {
                    return NotFound();
                }

                return Ok(updatedSalon);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating salon. SalonId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the salon.");
            };
        }
        /// <summary>
        /// Delete a salon by its ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which salon that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Salon>> DeleteMovieById(int Id)
        {
            var deleteSalon = await this._salonRepository.DeleteSalonById(Id);
            if (deleteSalon == null)
            {
                return NotFound();
            }
            return Ok(deleteSalon);
        }
    }
}
