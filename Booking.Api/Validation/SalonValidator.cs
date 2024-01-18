using Booking.Api.Entities;
using Ardalis.GuardClauses;
using Booking.Api.ExceptionHandler;

namespace Booking.Api.Validation
{
    public class SalonValidationException : ArgumentException
    {
        public SalonValidationException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class SalonValidator
    {
        public static void ValidateSalon(Salon salon)
        {
            Guard.Against.Null(salon, nameof(salon));
            ValidateStringProperty(salon.Name, nameof(salon.Name));

            ValdiateNonNegativeProperty(salon.NumberOfSeats, nameof(salon.NumberOfSeats));

            ValidateEnumProperty(salon.Status, nameof(salon.Status));
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
            where TEnum : Enum
        {
            Guard.Against.Null(value, propertyName);
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new CustomArgumentException($"Invalid value for {propertyName}", propertyName);
            }
        }
    }
}
