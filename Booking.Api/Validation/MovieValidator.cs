using Booking.Api.Entities;
using Ardalis.GuardClauses;

namespace Booking.Api.Validation
{
    public class MovieValidator
    {
        public static void ValidateMovie(Movie movie)
        {
            Guard.Against.Null(movie, nameof(movie));
            ValidateStringProperty(movie.Title, nameof(movie.Title));
            ValidateStringProperty(movie.Description, nameof(movie.Description));
            ValidateStringProperty(movie.Director, nameof(movie.Director));

            ValdiateNonNegativeProperty(movie.Hours, nameof(movie.Hours));
            ValdiateNonNegativeProperty(movie.Minutes, nameof(movie.Minutes));
            ValdiateNonNegativeProperty(movie.ReleaseYear, nameof(movie.ReleaseYear));
            ValdiateNonNegativeProperty(movie.AgeRestriction, nameof(movie.AgeRestriction));
            ValdiateNonNegativeProperty(movie.MaxShows, nameof(movie.MaxShows));

            ValidateEnumProperty(movie.Genre, nameof(movie.Genre));
            ValidateEnumProperty(movie.Language, nameof(movie.Language));
            ValidateEnumProperty(movie.Subtitle, nameof(movie.Subtitle));
        }

        private static void ValidateStringProperty(string? value, string propertyName)
        {
            Guard.Against.NullOrEmpty(value, propertyName);
        }

        private static void ValdiateNonNegativeProperty(int value, string propertyName)
        {
            Guard.Against.NegativeOrZero(value, propertyName);
        }

        private static void ValidateEnumProperty<TEnum>(TEnum value, string propertyName)
            where TEnum : struct
        {
            Guard.Against.Null(value, propertyName);
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new ArgumentOutOfRangeException(propertyName, $"Invalid value for {propertyName}");
            }
        }
    }
}
