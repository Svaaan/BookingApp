namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class ScheduleDTO
    {
        public DateTime Date { get; set; }
        public List<ShowDTO> Shows { get; set; }
    }
}
