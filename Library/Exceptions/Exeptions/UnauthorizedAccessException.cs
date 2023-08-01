using System.Net;

namespace Exceptions.Exeptions
{
    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException(string message) : base(message, null, HttpStatusCode.Unauthorized) { }
    }
}
