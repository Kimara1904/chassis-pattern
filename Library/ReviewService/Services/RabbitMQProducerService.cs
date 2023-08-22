using Exceptions.Exeptions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ReviewService.Interfaces;
using ReviewService.Model;
using System.Text;

namespace ReviewService.Services
{
    public class RabbitMQProducerService : IRabbitMQProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQProducerService(IConfiguration configuration, ConnectionFactory connectionFactory)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;

        }

        public void SendMailRequest(string header, string MailBody, string to)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //Direct Exchange Details like name and type of exchange
                    channel.ExchangeDeclare(_configuration["RabbitMqSettings:ExchangeName"], _configuration["RabbitMqSettings:ExchhangeType"]);
                    //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                    channel.QueueDeclare(queue: _configuration["RabbitMqSettings:QueueName"],
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    //Bind Queue with Exhange and routing details
                    channel.QueueBind(queue: _configuration["RabbitMqSettings:QueueName"]
                        , exchange: _configuration["RabbitMqSettings:ExchangeName"], routingKey: _configuration["RabbitMqSettings:RouteKey"]);
                    //Seriliaze object using Newtonsoft library
                    string productDetail = JsonConvert.SerializeObject(new MailModel { Header = header, Body = MailBody, To = to });
                    var body = Encoding.UTF8.GetBytes(productDetail);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    //publish msg
                    channel.BasicPublish(exchange: _configuration["RabbitMqSettings:ExchangeName"],
                                         routingKey: _configuration["RabbitMqSettings:RouteKey"],
                                         basicProperties: properties,
                                         body: body);
                }
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}
