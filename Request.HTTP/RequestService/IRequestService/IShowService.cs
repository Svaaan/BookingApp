using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IShowService
    {
        Task<bool> PostShow(ShowDTO show);
        Task<List<ScheduleDTO>> GetShow();
        Task<bool> RemoveShowById(int showId);
        Task<ShowDTO> EditShowById(ShowDTO show);
    }
}
