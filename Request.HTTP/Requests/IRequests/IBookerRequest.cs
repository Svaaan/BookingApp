namespace Request.Http.DTO.MovieTheatreDTO
{
    public interface IBookerRequest
    {
        Task<bool> RequestBooking(BookerDTO booker);
    }
}
