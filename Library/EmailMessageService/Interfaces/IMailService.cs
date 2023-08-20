namespace EmailMessageService.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(string header, string body, string to);
    }
}
