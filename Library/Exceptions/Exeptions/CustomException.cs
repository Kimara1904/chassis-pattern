using System.Net;

namespace Exceptions.Exeptions
{
    public class CustomException : Exception
    {
        public List<string>? ErrorMessages { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(string message, List<string>? errorMessages = default,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            ErrorMessages = errorMessages;
            StatusCode = statusCode;
        }
    }
}
