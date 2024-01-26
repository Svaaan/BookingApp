using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly ILogger<ShowController> _logger;
        private readonly IShowRepository _showRepository;

        public ShowController(ILogger<ShowController> logger, IShowRepository showRepository)
        {
            _logger = logger;
            _showRepository = showRepository;
        }

        /// <summary>
        /// Create a show based on the Dto inputs
        /// </summary>
        /// <param name="showDto"></param>
        /// <returns>The created show</returns>
        /// <response code="201">Returns the newly created Show.</response>
        /// <response code="400">If the Show values are not valid.</response>
        /// <response code="500">Processing failure.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Show>> CreateShow([FromBody] ShowUpsertDto showDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createShow = await _showRepository.CreateShow(showDto);

                return Ok(createShow);
            }
            catch (ShowValidationException ex)
            {
                // Log the exception if needed
                _logger.LogError(ex, "Show validation failed.");

                // Return a more user-friendly error response
                return BadRequest(new { error = ex.Message, propertyName = ex.ParamName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a show.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Show>> UpdateShow(int id, [FromBody] ShowUpsertDto updateShow)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var show = await this._showRepository.UpdateShow(id, updateShow);
                return Ok(show);
            }
            catch (ShowUpsertDtoException ex)
            {
                // Log the exception if needed
                _logger.LogError(ex, "Show validation failed.");

                // Return a more user-friendly error response
                return BadRequest(new { error = ex.Message, propertyName = ex.ParamName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a show.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Show>> DeleteShow(int id)
        {
            var deleteShow = await _showRepository.DeleteShowById(id);
            if (deleteShow == null)
            {
                return BadRequest();
            }
            return Ok(deleteShow);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShowDetailsDto>> GetShowById(int id)
        {
            var show = await _showRepository.GetShowById(id);
            if (show == null)
            {
                return BadRequest();
            }
            return Ok(show);
        }

        [HttpGet("schedule")]
        public async Task<ActionResult<List<Schedule>>> GetAllShows()
        {
            var shows = await _showRepository.GetShowsByDateAndHours();
            return Ok(shows);
        }
    }
}
