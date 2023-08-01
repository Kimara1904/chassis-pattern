namespace Exceptions.Exeptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message, null, System.Net.HttpStatusCode.NotFound) { }
    }
}
