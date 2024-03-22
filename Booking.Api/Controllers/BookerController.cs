using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Booking.Api.Entities;
using Booking.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Booking.Api.Repositories;
using System.Reflection;

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
        /// <returns>The Booker booker.</returns>
        /// <response code="200">Returns the newly created booker.</response>
        /// <response code="400">If the boooker is not valid or an error occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booker>> PostBooker([FromBody] Booker booker)
        {
            if (booker == null)
            {
                return BadRequest("Booker object is null.");
            }

            PropertyInfo[] properties = typeof(Booker).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(booker) == null)
                {
                    return BadRequest($"Property {property.Name} is null.");
                }
            }

            var createBooker = await _bookerRepository.CreateBookerAsync(booker);
            return Ok(createBooker);
        }
        /// <summary>
        /// Lists all bookers.
        /// </summary>
        /// <returns>A list of booker.</returns>
        /// <response code="200">Returns the list of booker.</response>
        /// <response code="500">If an error occurs while retrieving the booker.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Booker>>> ListAllBookers()
        {
            var bookerList = await _bookerRepository.GetAllBookersAsync();
            if (bookerList == null)
            {
                return NotFound();
            }
            return Ok(bookerList);
        }

        /// <summary>
        /// Delete a booker ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a message specifying which booker that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Booker>> DeleteBookerById(int id)
        {
            var deleteBooker = await this._bookerRepository.DeletebookerByIdAsync(id);
            if (deleteBooker == null)
            {
                return NotFound();
            }
            return Ok(deleteBooker);
        }

        /// <summary>
        /// Retrieve a specific Booker from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Booker>> GetBookerById(int id)
        {
            var booker = await _bookerRepository.GetBookerById(id);

            if (booker == null)
            {
                return NotFound();
            }

            return Ok(booker);
        }

        /// <summary>
        /// Update a booker by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateBooker"></param>
        /// <returns>Returns the updated booker.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Booker), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Booker>> UpdateBooker(int id, [FromBody] Booker updateBooker)
        {
            try
            {
                // Ensure the provided ID matches the ID in the updateBooker
                if (id != updateBooker.Id)
                {
                    return BadRequest("Mismatched booker ID in the request.");
                }

                var updatedBooker = await _bookerRepository.UpdateBookerById(id, updateBooker);
                if (updatedBooker == null)
                {
                    return NotFound();
                }

                return Ok(updatedBooker);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating booker. bookerId: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the booker.");
            }
        }
    }
}
