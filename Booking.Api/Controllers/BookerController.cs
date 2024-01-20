using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Ensure this using statement is present for ILogger
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookerController : ControllerBase
    {
        private readonly IBookerRepository _bookerRepository;
        private readonly ILogger<BookerController> _logger;

        public BookerController(IBookerRepository bookerRepository, ILogger<BookerController> logger)
        {
            _bookerRepository = bookerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create a new movie.
        /// </summary>
        /// <param name="booker">The movie to create.</param>
        /// <returns>The created movie.</returns>
        /// <response code="200">Returns the newly created movie.</response>
        /// <response code="400">If the movie is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booker>> PostBooker([FromBody] Booker booker)
        {
            var createBooker = await _bookerRepository.CreateBookerAsync(booker);

            return Ok(createBooker);
        }
    }
}
