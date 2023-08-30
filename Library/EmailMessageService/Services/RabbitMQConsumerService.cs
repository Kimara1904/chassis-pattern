using EmailMessageService.Interfaces;
using EmailMessageService.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EmailMessageService.Services
{
    public class RabbitMQConsumerService : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private IMailService _mailService;
        private IConfiguration _configuration;
        private ConnectionFactory _connectionFactory;

        public RabbitMQConsumerService(IMailService mailService, IConfiguration configuration, ConnectionFactory connectionFactory)
        {
            _mailService = mailService;
            _configuration = configuration;
            _connectionFactory = connectionFactory;

            // create connection
            while (true)
            {
                try
                {
                    _connection = _connectionFactory.CreateConnection();
                    break;
                }
                catch (Exception)
                {
                    Thread.Sleep(60000);
                }
            }
            // create channel
            _channel = _connection.CreateModel();
            //Direct Exchange Details like name and type of exchange
            _channel.ExchangeDeclare(_configuration["RabbitMqSettings:ExchangeName"], _configuration["RabbitMqSettings:ExchhangeType"]);
            //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
            _channel.QueueDeclare(queue: _configuration["RabbitMqSettings:QueueName"],
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
            arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            _channel.QueueBind(queue: _configuration["RabbitMqSettings:QueueName"],
                exchange: _configuration["RabbitMqSettings:ExchangeName"], routingKey: _configuration["RabbitMqSettings:RouteKey"]);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                // acknowledge the received message
                _channel.BasicAck(ea.DeliveryTag, false);
                //Deserilized Message
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var mail = JsonConvert.DeserializeObject<MailModel>(message);

                if (mail != null)
                {
                    _mailService.SendEmail(mail.Header, mail.Body, mail.To);
                }

            };

            _channel.BasicConsume(_configuration["RabbitMqSettings:QueueName"], false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
