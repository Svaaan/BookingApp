using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
using Booking.Api.Validation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Booking.Api.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext _context;
        private readonly ILogger<MovieRepository> _logger;

        public ReservationRepository(CinemaDbContext context, ILogger<MovieRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Reservation> CreateReservation(ReservationDto reservationDto)
        {
            try
            {
                var show = await _context.shows.FirstOrDefaultAsync(s => s.ID == reservationDto.ShowId);

                if (show.AvailableSeats < reservationDto.BookedSeats)
                {
                    throw new Exception("Not enough seats are available for booking");
                }

                var reservation = new Reservation
                {
                    ShowId = reservationDto.ShowId,
                    BookerEmail = reservationDto.BookerEmail,
                    BookedSeats = reservationDto.BookedSeats,
                };

                ReservationValidator.ValidateReservation(reservation);

                _context.reservations.Add(reservation);
                await _context.SaveChangesAsync();

                show.AvailableSeats -= reservationDto.BookedSeats;
                await _context.SaveChangesAsync();

                return reservation;
            }
            catch (ReservationValidationException ex)
            {
                // Handle validation exception
                _logger.LogError(ex, "Validation error while creating a reservation.");
                throw new Exception($"Validation failed: {ex.Message}");
            }
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            var reservationList = await _context.reservations.ToListAsync();
            return reservationList;
        }

        public async Task<Reservation> GetReservationById(int Id)
        {
            try
            {
                var getReservation = await _context.reservations.FindAsync(Id);
                if (getReservation == null)
                {
                    _logger.LogInformation($"Couldnt find a reservation in the database with the ID: {Id}.");
                }
                return getReservation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object");
                throw;
            }
        }

        public async Task<Reservation> UpdateReservation(int Id, ReservationDto updateReservation)
        {
            var reservation = await _context.reservations.FindAsync(Id);

            if (reservation == null)
            {
                _logger.LogError($"No reservation found with the Id: {Id}");
                return null;
            }

            var showId = reservation.ShowId;

            // Load the show from the database
            var show = await _context.shows.FindAsync(showId);

            if (show == null)
            {
                _logger.LogError($"No show found with the ID: {showId}");
                return null;
            }

            var seatDifference = updateReservation.BookedSeats - reservation.BookedSeats;

            if (seatDifference > 0 && show.AvailableSeats - seatDifference < 0)
            {
                throw new Exception("Not enough seats are available for the requested update");
            }

            // Update reservation properties
            reservation.ShowId = updateReservation.ShowId;
            reservation.BookerEmail = updateReservation.BookerEmail;
            reservation.BookedSeats = updateReservation.BookedSeats;

            ReservationValidator.ValidateReservation(reservation);

            // Adjust available seats based on seat difference
            show.AvailableSeats -= seatDifference;

            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> DeleteReservation(int Id)
        {
            try
            {
                var deleteReservation = await _context.reservations.FindAsync(Id);

                if (deleteReservation != null)
                {
                    _context.reservations.Remove(deleteReservation);
                    await _context.SaveChangesAsync();
                }
                return deleteReservation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting movie. MovieId: {MovieId}", Id);
                throw;
            }

        }
    }
}
