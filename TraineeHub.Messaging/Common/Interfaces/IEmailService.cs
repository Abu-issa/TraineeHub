using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.ApplictionRMQ.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
