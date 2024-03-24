using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(ILogger<ReservationController> logger , IReservationRepository reservationRepository)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Create a reservation based on the Dto inputs
        /// </summary>
        /// <param name="reservationDto"></param>
        /// <returns>The reservation show</returns>
        /// <response code="201">Returns the newly created Reservation.</response>
        /// <response code="400">If the reservation values are not valid.</response>
        /// <response code="500">Processing failure.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody]CreateReservationDTO reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reservation = new Reservation
            {
               ShowId = reservationDto.ShowId,
               BookedSeats = reservationDto.BookedSeats,
               BookerId = reservationDto.BookerId,
               ReservationTime = DateTime.UtcNow
        };
            var createReservation = await this._reservationRepository.CreateReservation(reservation);
            return Ok(createReservation);
        }

        /// <summary>
        /// Lists all reservations.
        /// </summary>
        /// <returns>A list of reservation.</returns>
        /// <response code="200">Returns the list of reservations.</response>
        /// <response code="500">If an error occurs while retrieving the reservations.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Reservation>>> ListAllReservations()
        {
            var reservationList = await this._reservationRepository.GetAllReservations();

            if (reservationList == null)
            {
                return NotFound();
            }
            return Ok(reservationList);
        }

        /// <summary>
        /// Get the reservation object.
        /// </summary>
        /// <returns>An object of the reservation.</returns>
        /// <response code="200">Returns the reservation object.</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        /// <summary>
        /// Update a reservation by entering it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateReservation"></param>
        /// <returns>Returns the updated Reservation.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, [FromBody] ReservationDto updateReservation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var reservation = await this._reservationRepository.UpdateReservation(id, updateReservation);
                return Ok(reservation);
            }
            catch (ShowUpsertDtoException ex)
            {
                // Log the exception if needed
                _logger.LogError(ex, "Reservation validation failed.");

                // Return a more user-friendly error response
                return BadRequest(new { error = ex.Message, propertyName = ex.ParamName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a reservation.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating a reservation.\n" + ex.Message);
            }
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return a object specifying which reservation that has been deleted</response>
        /// <response code="404">Return a not found if incorrect id and/or id doesnt exist</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var deleteReservation = await _reservationRepository.DeleteReservation(id);
            if (deleteReservation == null)
            {
                return NotFound();
            }
            return Ok(deleteReservation);
        }
    }
}
