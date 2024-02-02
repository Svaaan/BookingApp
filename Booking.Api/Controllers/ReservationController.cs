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

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody]ReservationDto reservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createReservation = await this._reservationRepository.CreateReservation(reservationDto);
            return Ok(createReservation);
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> ListAllReservations()
        {
            var reservationList = await this._reservationRepository.GetAllReservations();

            if (reservationList == null)
            {
                return NotFound();
            }
            return Ok(reservationList);
        }

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

        [HttpPut("{id:int}")]
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var deleteReservation = await _reservationRepository.DeleteReservation(id);
            if (deleteReservation == null)
            {
                return BadRequest();
            }
            return Ok(deleteReservation);
        }
    }
}
