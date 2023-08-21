namespace ReviewService.Interfaces
{
    public interface IRabbitMQProducerService
    {
        void SendMailRequest(string header, string body, string to);
    }
}
