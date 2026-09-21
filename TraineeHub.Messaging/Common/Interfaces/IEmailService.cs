using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Messaging.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
