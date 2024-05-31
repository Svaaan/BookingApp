using Booking.Api.Entities;
using Ardalis.GuardClauses;

namespace Booking.Api.Validation
{
    public class MovieTheatreValidationException : ArgumentException
    {
        public MovieTheatreValidationException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class MovieTheatreValidator
    {
        public static void ValidateMovieTheatre(MovieTheatre movieTheatre)
        {
            Guard.Against.Null(movieTheatre, nameof(movieTheatre));
            ValidateStringProperty(movieTheatre.Name, nameof(movieTheatre.Name));
         

            ValdiateNonNegativeProperty(movieTheatre.CompanyId, nameof(movieTheatre.CompanyId));
        }

        private static void ValidateStringProperty(string? value, string propertyName)
        {
            Guard.Against.NullOrEmpty(value, propertyName);
        }

        private static void ValdiateNonNegativeProperty(int value, string propertyName)
        {
            Guard.Against.NegativeOrZero(value, propertyName);
        }
        
    }
}
