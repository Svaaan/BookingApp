using Ardalis.GuardClauses;
using Booking.Api.Entities;

namespace Booking.Api.Validation
{
    public class ShowValidationException : ArgumentException
    {
        public ShowValidationException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class ShowValidator
    {
        public static void ValidateShow(Show show)
        {
            Guard.Against.Null(show, nameof(show));
            ValidateNonNegativeProperty(show.MovieId, nameof(show.MovieId));
            ValidateNonNegativeProperty(show.SalonId, nameof(show.SalonId));
            ValidateNonNegativeProperty(show.AvailableSeats, nameof(show.AvailableSeats));
            ValidateDateTimeProperty(show.StartTime, nameof(show.StartTime));
            ValidateDateTimeProperty(show.EndTime, nameof(show.EndTime));
        }

        private static void ValidateNonNegativeProperty(int value, string propertyName)
        {
            Guard.Against.NegativeOrZero(value,propertyName);
        }

        private static void ValidateDateTimeProperty(DateTime value, string propertyName)
        {
            if(value < DateTime.UtcNow)
            {
                throw new ShowValidationException($"{propertyName} must be in the future.", propertyName);
            }
        }
    }
}
