using Ardalis.GuardClauses;
using Booking.Api.Entities;

namespace Booking.Api.Validation
{
    public class ReservationValidationException : ArgumentException
    {
        public ReservationValidationException(string message, string paramName)
            : base(message, paramName)
        {
        }
    }

    public class ReservationValidator
    {
        private static readonly TimeSpan FutureDateWindow = TimeSpan.FromMinutes(5);

        public static void ValidateReservation(Reservation reservation)
        {
            Guard.Against.Null(reservation, nameof(reservation));
            ValidateNonNegativeProperty(reservation.ShowId, nameof(reservation.ShowId));
            ValidateNonNegativeProperty(reservation.BookedSeats, nameof(reservation.BookedSeats));
            ValidateNonEmptyStringProperty(reservation.BookerEmail, nameof(reservation.BookerEmail));
            ValidateDateTimeProperty(reservation.ReservationTime, nameof(reservation.ReservationTime));
        }

        private static void ValidateNonNegativeProperty(int value, string propertyName)
        {
            Guard.Against.NegativeOrZero(value, propertyName);
        }

        private static void ValidateNonEmptyStringProperty(string value, string propertyName)
        {
            Guard.Against.NullOrEmpty(value, propertyName);
        }

        private static void ValidateDateTimeProperty(DateTime value, string propertyName)
        {
            var minDateTime = DateTime.UtcNow.Add(-FutureDateWindow);
            var maxDateTime = DateTime.UtcNow.Add(FutureDateWindow);
            if (value < minDateTime || value > maxDateTime)
            {
                throw new ReservationValidationException($"{propertyName} must be within the allowed time window.", propertyName);
            }
        }
    }
}
