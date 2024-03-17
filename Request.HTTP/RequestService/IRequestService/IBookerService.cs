namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public interface IBookerService
    {
        Task<bool> RequestBooking(BookerDTO booker);
    }
}
