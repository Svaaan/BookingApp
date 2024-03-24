using System.Text.Json.Serialization;

namespace Booking.Api.Entities.DTO
{
    public class SalonDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AvailableSeats { get; set; }
        public int MovieTheatreId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}
