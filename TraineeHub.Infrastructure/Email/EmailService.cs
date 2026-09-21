using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using TraineeHub.Messaging.Common.Interfaces;

namespace TraineeHub.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration)
        {
            _apiKey = "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";
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var client = new SendGridClient(_apiKey);

            var from = new EmailAddress("mohammadabuissa253@gmail.com", "TraineeHub01"); 
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(
                from,
                toEmail,
                subject,
                plainTextContent: "",
                htmlContent: body
            );

            await client.SendEmailAsync(msg);
        }
    }
}