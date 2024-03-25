using Booking.Api.Entities;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task<List<Reservation>> GetAllReservations();
        Task<Reservation> GetReservationById(int Id);
        Task<Reservation> UpdateReservation(int Id, Reservation reservationDto);
        Task<Reservation> DeleteReservation(int Id);
    }
}
