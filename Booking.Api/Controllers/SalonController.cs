using Booking.Api.Entities;
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
                if (salon == null)
                {
                    return BadRequest("Invalid salon data entered.");
                }

                var createSalonSuccess = await _salonRepository.CreateSalonAsync(salon);

                if (createSalonSuccess != null)
                {
                    return CreatedAtAction(nameof(PostSalon), new { id = salon.ID }, salon);
                }
                else
                {
                    return StatusCode(500, "Failed to create salon");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
