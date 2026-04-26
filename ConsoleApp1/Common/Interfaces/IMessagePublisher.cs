using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.ApplictionRMQ.Common.Events;

namespace TraineeHub.ApplictionRMQ.Common.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(SubmissionCreatedEvent message);
    }
}
