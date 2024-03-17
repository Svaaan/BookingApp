using Booking.Api.Entities.DTO;

namespace Request.Http.DTO.MovieTheatreDTO
{
    public class Schedule
    {
        public DateTime Date { get; set; }
        public List<ShowDto> Shows { get; set; }
    }
}
