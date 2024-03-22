namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public interface IBookerService
    {
        Task<bool> PostBooking(BookerDTO booker);
        Task<List<BookerDTO>> GetBooker();
    }
}
