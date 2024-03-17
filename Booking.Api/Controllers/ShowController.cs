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

        /// <summary>
        /// Update a show by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateShow"></param>
        /// <returns>Returns the updated Show.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                _logger.LogError(ex, "An error occurred while updating a show.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Delete a show
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a object specifying which show that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Show>> DeleteShow(int id)
        {
            var deleteShow = await _showRepository.DeleteShowById(id);
            if (deleteShow == null)
            {
                return NotFound();
            }
            return Ok(deleteShow);
        }

        /// <summary>
        /// Get the show object.
        /// </summary>
        /// <returns>An object of the show.</returns>
        /// <response code="200">Returns the show object.</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShowDetailsDto>> GetShowById(int id)
        {
            var show = await _showRepository.GetShowById(id);
            if (show == null)
            {
                return NotFound();
            }
            return Ok(show);
        }

        /// <summary>
        /// Lists all shows.
        /// </summary>
        /// <returns>A list of shows.</returns>
        /// <response code="200">Returns the list of shows.</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        /// <response code="500">If an error occurs while retrieving the shows.</response>
        [HttpGet("schedule")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Schedule>>> GetAllShows()
        {
            var showList = await _showRepository.GetShowsByDateAndHours();
            if (showList == null)
            {
                return NotFound();
            }
            return Ok(showList);
        }
    }
}
