using Booking.Api.Entities.DTO;

namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class Schedule
    {
        public DateTime Date { get; set; }
        public List<ShowDto> Shows { get; set; }
    }
}
