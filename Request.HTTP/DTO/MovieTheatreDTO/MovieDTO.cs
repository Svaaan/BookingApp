using System.Text.Json.Serialization;

namespace Request.Http.DTO.MovieTheatreDTO
{
    public class Movie
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int ReleaseYear { get; set; }
        public int AgeRestriction { get; set; }
        public int MaxShows { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Genres Genre { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Languages Language { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Subtitles Subtitle { get; set; }
    }

    public enum Genres
    {
        Romance,
        Action,
        Horror,
        Adventure,
        Family,
        Drama,
        Crime,
        Fantasy,
        Thiller

    }

    public enum Languages
    {
        English,
        Swedish
    }

    public enum Subtitles
    {
        English,
        Swedish
    }
}
