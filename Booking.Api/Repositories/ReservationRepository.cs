using Booking.Api.Data;
using Booking.Api.Entities;
using Booking.Api.Entities.DTO;
using Booking.Api.Repositories.Interfaces;
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

            _context.reservations.Add(reservation);
            await _context.SaveChangesAsync();

            show.AvailableSeats -= reservationDto.BookedSeats;
            await _context.SaveChangesAsync();
            
            return reservation;
        }
    }
}
