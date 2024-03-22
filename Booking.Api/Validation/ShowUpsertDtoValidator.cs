using Ardalis.GuardClauses;
using Booking.Api.Entities.DTO;

namespace Booking.Api.Validation
{
    public class ShowUpsertDtoException : ArgumentException
    {
        public ShowUpsertDtoException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class ShowUpsertDtoValidator
    {
        public static void ValidateShowUpsertDto(ShowUpsertDto showDto)
        {
            Guard.Against.Null(showDto, nameof(showDto));
            ValidateNonNegativeProperty(showDto.MovieId, nameof(showDto.MovieId));
            ValidateNonNegativeProperty(showDto.SalonId, nameof(showDto.SalonId));
            ValidateDateTimeProperty(showDto.StartTime, nameof(showDto.StartTime));
            ValidateDateTimeProperty(showDto.EndTime, nameof(showDto.EndTime));
        }

        private static void ValidateNonNegativeProperty(int value, string propertyName)
        {
            try
            {
                Guard.Against.NegativeOrZero(value, propertyName);
            }
            catch (Exception ex)
            {
                // Throw the exception to be caught in the calling method
                throw new ShowUpsertDtoException($"Validation failed for {propertyName}: {ex.Message}", propertyName);
            }
        }
        private static void ValidateDateTimeProperty(DateTime value, string propertyName)
        {
            if (value < DateTime.UtcNow)
            {
                throw new ShowValidationException($"{propertyName} must be in the future.", propertyName);
            }
        }
    }
}

