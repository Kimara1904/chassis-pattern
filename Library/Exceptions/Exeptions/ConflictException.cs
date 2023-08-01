using System.Net;

namespace Exceptions.Exeptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message) : base(message, null, HttpStatusCode.Conflict) { }
    }
}
