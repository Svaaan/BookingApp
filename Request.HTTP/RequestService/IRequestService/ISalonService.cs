using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface ISalonService
    {
        Task<bool> PostSalon(SalonDTO salon);
        Task<List<SalonDTO>> GetSalon();
        Task<bool> RemoveSalonById(int salonId);
        Task<SalonDTO> EditSalonById(SalonDTO salon);
    }
}
