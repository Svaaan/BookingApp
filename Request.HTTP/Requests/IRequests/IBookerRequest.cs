namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public interface IBookerRequest
    {
        Task<bool> RequestBooking(BookerDTO booker);
    }
}
