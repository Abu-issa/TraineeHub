using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.ApplictionRMQ.Common.Events
{
    public class SubmissionCreatedEvent
    {
        public int SubmissionId { get; set; }
        public string TraineeName { get; set; }
        public string Topic { get; set; }
    }
}
