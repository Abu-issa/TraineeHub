using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TraineeHub.ApplictionRMQ.Common.Events;
using TraineeHub.ApplictionRMQ.Common.Interfaces;
using TraineeHub.Infrastructure.Email;

namespace TraineeHub.Infrastructure.Messaging
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IEmailService _emailService;

        public RabbitMqConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🚀 Consumer Started...");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true // 🔥 مهم للأداء
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "submission-queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (sender, e) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(e.Body.ToArray());

                    var message = JsonSerializer.Deserialize<SubmissionCreatedEvent>(json);

                    if (message == null)
                    {
                        Console.WriteLine("❌ Invalid message received");
                        return;
                    }

                    Console.WriteLine($"📩 Received Submission: {message.SubmissionId}");

                    // 🔥 HTML Email body
                    var body = EmailTemplates.SubmissionReceived(
                        message.TraineeName,
                        message.Topic
                    );

                    await _emailService.SendEmail(
                        to: "mohammadabuissa253@gmail.com",
                         subject: "Submission Received 🎉",
                             body: body
                            );

                    Console.WriteLine("📧 Email sent successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            };

            channel.BasicConsume(
                queue: "submission-queue",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine("🎧 Listening to queue...");

            return Task.CompletedTask;
        }
    }
}