using System.Text.Json.Serialization;

namespace Request.HTTP.DTO.MovieTheatreDTO
{
    public class SalonDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int AvailableSeats { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }

    public enum Status
    {
        Open,
        Closed,
        FullBooked
    }
}
