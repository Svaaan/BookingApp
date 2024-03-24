using Booking.Api.Entities;
using Ardalis.GuardClauses;

namespace Booking.Api.Validation
{
    public class CompanyException : ArgumentException
    {
        public CompanyException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class CompanyValidator
    {
        public static void ValidateCompany(Company company)
        {
            Guard.Against.Null(company, nameof(company));
            ValidateStringProperty(company.CompanyName, nameof(company.CompanyName));
            ValidateStringProperty(company.Email, nameof(company.Email));
          
        }

        private static void ValidateStringProperty(string? value, string propertyName)
        {
            Guard.Against.NullOrEmpty(value, propertyName);
        }
    }
}
