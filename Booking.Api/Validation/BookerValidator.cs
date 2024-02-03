using Booking.Api.Entities;
using Ardalis.GuardClauses;
using Booking.Api.ExceptionHandler;

namespace Booking.Api.Validation
{
    public class BookerException : ArgumentException
    {
        public BookerException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class BookerValidator
    {
        public static void ValidateBooker(Booker booker)
        {
            Guard.Against.Null(booker, nameof(booker));
            ValidateStringProperty(booker.Name, nameof(booker.Name));
            ValidateStringProperty(booker.LastName, nameof(booker.LastName));
            ValidateStringProperty(booker.Email, nameof(booker.Email));
            ValidateStringProperty(booker.PhoneNumber, nameof(booker.PhoneNumber));
        }

        private static void ValidateStringProperty(string? value, string propertyName)
        {
            Guard.Against.NullOrEmpty(value, propertyName);
        }
    }
}
