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
            ValidateNonNegativeProperty(show.MovieID, nameof(show.MovieID));
            ValidateNonNegativeProperty(show.SalonID, nameof(show.SalonID));
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
            if(value < DateTime.Now)
            {
                throw new ShowValidationException($"{propertyName} must be in the future.", propertyName);
            }
        }
    }
}
