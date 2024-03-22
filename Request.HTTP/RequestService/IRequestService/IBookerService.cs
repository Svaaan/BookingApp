namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public interface IBookerService
    {
        Task<bool> PostBooking(BookerDTO booker);
        Task<List<BookerDTO>> GetBooker();
        Task<bool> RemoveBookerById(int bookerId);
        Task<BookerDTO> EditBookerById(BookerDTO booker);
    }
}
