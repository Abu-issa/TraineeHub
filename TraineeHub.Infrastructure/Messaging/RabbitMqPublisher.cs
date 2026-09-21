using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TraineeHub.Messaging.Common.Events;
using TraineeHub.Messaging.Common.Interfaces;

namespace TraineeHub.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqPublisher()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public void Publish(SubmissionCreatedEvent message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            
            channel.QueueDeclare(
                queue: "submission-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

          
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

           
            channel.BasicPublish(
                exchange: "",
                routingKey: "submission-queue",
                basicProperties: null,
                body: body);
        }
    }
}