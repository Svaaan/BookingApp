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
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(CinemaDbContext context, ILogger<ReservationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            try
            {
                var show = await _context.shows.FirstOrDefaultAsync(s => s.Id == reservation.ShowId);

                if (show.AvailableSeats < reservation.BookedSeats)
                {
                    throw new Exception("Not enough seats are available for booking");
                }

                ReservationValidator.ValidateReservation(reservation);

                _context.reservations.Add(reservation);
                await _context.SaveChangesAsync();

                show.AvailableSeats -= reservation.BookedSeats;
                await _context.SaveChangesAsync();

                var createdReservation = await _context.reservations.Include(r => r.Booker).Where(r => r.Id == reservation.Id).FirstAsync();
                var receipt = new Receipt(createdReservation, show);

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
            var reservationList = await _context.reservations.Include(r => r.Booker).ToListAsync();
            return reservationList;
        }

        public async Task<Reservation> GetReservationById(int Id)
        {
            try
            {
                var getReservation = await _context.reservations.Include(r => r.Booker).Where(r => r.Id == Id).FirstAsync();
                if (getReservation == null)
                {
                    _logger.LogInformation($"Couldnt find a reservation in the database with the ID: {Id}.");
                }
                return getReservation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred when Retrieving the object with ID: {Id}", Id);
                throw;
            }
        }

        public async Task<Reservation> UpdateReservation(int Id, Reservation updateReservation)
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
