using Booking.Api.Entities;
using System.Text.Json.Serialization;

namespace Request.HTTP.DTO.MovieTheatreDTO
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
