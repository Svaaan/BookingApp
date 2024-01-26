using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
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

        [HttpPost]
        public async Task<ActionResult<Show>> CreateShow([FromBody] CreateShowDto showDto)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a show.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
