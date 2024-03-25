using System.Text.Json.Serialization;

namespace Booking.Api.Entities
{
    public class Salon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AvailableSeats { get; set; }
        public int MovieTheatreId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public MovieTheatre MovieTheatre { get; set; }
    }

    public enum Status
    {
        Open,
        Closed,
        FullBooked
    }
}
