using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using TraineeHub.ApplictionRMQ.Common.Interfaces;

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

            var from = new EmailAddress("mohammadabuissa253@gmail.com", "TraineeHub01"); // ✅ verified
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