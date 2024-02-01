using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        /// Create a new booker.
        /// </summary>
        /// <param name="booker">The booker to create.</param>
        /// <returns>The Booker movie.</returns>
        /// <response code="200">Returns the newly created booker.</response>
        /// <response code="400">If the boooker is not valid or an error occurs.</response>
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
