using Booking.Api.Entities;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservation(ReservationDto reservationDto);
        Task<List<Reservation>> GetAllReservations();
        Task<Reservation> GetReservationById(int Id);
        Task<Reservation> UpdateReservation(int Id, ReservationDto reservationDto);
    }
}
