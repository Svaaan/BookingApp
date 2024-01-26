using System.Text.Json.Serialization;

namespace Booking.Api.Entities.DTO
{
    public class ShowDetailsDto
    {
        public int ShowId { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string MovieDescription { get; set; }
        public string MovieDirector { get; set; }

        public int Hours { get; set; }
        public int minutes { get; set; }
        public int ReleaseYear { get; set; }
        public int AgeRestriction { get; set; }

        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Genres Genre { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Languages Language { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Subtitles Subtitle { get; set; }
    }
}
