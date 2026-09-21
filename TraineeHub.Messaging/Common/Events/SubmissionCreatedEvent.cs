using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Messaging.Common.Events
{
    public class SubmissionCreatedEvent
    {
        public Guid SubmissionId { get; set; }
        public string TraineeName { get; set; }
        public string Topic { get; set; }
    }
}
