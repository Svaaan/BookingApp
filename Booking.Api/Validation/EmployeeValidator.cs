using Booking.Api.Entities;
using Ardalis.GuardClauses;

namespace Booking.Api.Validation
{
    public class EmployeeException : ArgumentException
    {
        public EmployeeException(string message, string paramName)
        : base(message, paramName)
        {
        }
    }

    public class EmployeeValidator
    {
        public static void ValidateEmployee(Employee employee)
        {
            Guard.Against.Null(employee, nameof(employee));
            ValidateStringProperty(employee.Name, nameof(employee.Name));
            ValidateStringProperty(employee.LastName, nameof(employee.LastName));
            ValidateStringProperty(employee.Email, nameof(employee.Email));
            ValidateStringProperty(employee.Password, nameof(employee.Password));

            ValdiateNonNegativeProperty(employee.CompanyId, nameof(employee.CompanyId));
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
