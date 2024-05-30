using Request.HTTP.DTO.MovieTheatreDTO;

namespace Request.HTTP.RequestService.IRequestService
{
    public interface IBookerService
    {
        Task<BookerDTO> PostBooking(BookerDTO booker);
        Task<List<BookerDTO>> GetBooker();
        Task<bool> RemoveBookerById(int bookerId);
        Task<BookerDTO> EditBookerById(BookerDTO booker);
    }
}
