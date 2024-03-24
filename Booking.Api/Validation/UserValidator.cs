using Booking.Api.Entities;
using Ardalis.GuardClauses;

namespace Booking.Api.Validation
{
    public class UserException : ArgumentException
    {
        public UserException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class UserValidator
    {
        public static void ValidateUser(User user)
        {
            Guard.Against.Null(user, nameof(user));
            ValidateStringProperty(user.Name, nameof(user.Name));
            ValidateStringProperty(user.LastName, nameof(user.LastName));
            ValidateStringProperty(user.Email, nameof(user.Email));
            ValidateStringProperty(user.Password, nameof(user.Password));

            ValdiateNonNegativeProperty(user.CompanyId, nameof(user.CompanyId));
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
