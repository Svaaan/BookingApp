using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IReservationService
    {
        Task<bool> PostReservation(ReservationDTO reservation);
        Task<List<ReservationDTO>> GetReservation();
        Task<bool> RemoveReservationById(int bookerId);
        Task<ReservationDTO> EditReservationById(ReservationDTO booker);
    }
}
