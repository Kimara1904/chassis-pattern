using System.Net;

namespace Exceptions.Exeptions
{
    public class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException(string message, List<string>? errorMessages = default)
            : base(message, errorMessages, HttpStatusCode.InternalServerError) { }
    }
}
