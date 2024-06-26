﻿using Booking.Api.Data;
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

                if (show == null)
                {
                    throw new Exception($"Show with ID {reservation.ShowId} not found.");
                }

                if (show.AvailableSeats < reservation.BookedSeats)
                {
                    throw new Exception("Not enough seats are available for booking.");
                }

                ReservationValidator.ValidateReservation(reservation);

                _context.reservations.Add(reservation);
                await _context.SaveChangesAsync();

                show.AvailableSeats -= reservation.BookedSeats;
                await _context.SaveChangesAsync();

                var createdReservation = await _context.reservations
                                                        .Include(r => r.Booker)
                                                        .FirstOrDefaultAsync(r => r.Id == reservation.Id);

                if (createdReservation == null)
                {
                    throw new Exception($"Failed to retrieve the created reservation with ID {reservation.Id}.");
                }

                var receipt = new Receipt(createdReservation, show);

                return createdReservation;
            }
            catch (ReservationValidationException ex)
            {
                _logger.LogError(ex, "Validation error while creating a reservation.");
                throw new Exception($"Validation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a reservation.");
                throw;
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

            reservation.ShowId = updateReservation.ShowId;
            reservation.BookedSeats = updateReservation.BookedSeats;

            ReservationValidator.ValidateReservation(reservation);

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
                    var show = await _context.shows.FindAsync(deleteReservation.ShowId);
                    if (show != null)
                    {
                        show.AvailableSeats += deleteReservation.BookedSeats;
                        _context.shows.Update(show);
                    }

                    _context.reservations.Remove(deleteReservation);
                    await _context.SaveChangesAsync();
                }

                return deleteReservation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting reservation. ReservationId: {ReservationId}", Id);
                throw;
            }
        }
    }
}
