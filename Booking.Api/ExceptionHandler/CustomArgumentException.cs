namespace Booking.Api.ExceptionHandler
{
    public class CustomArgumentException : ArgumentException
    {
        public CustomArgumentException()
        {
        }

        public CustomArgumentException(string message)
        : base(message)
        {
        }

        public CustomArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CustomArgumentException(string message, string paramName)
            : base(message, paramName)
        {
        }

        public CustomArgumentException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }
    }
}
