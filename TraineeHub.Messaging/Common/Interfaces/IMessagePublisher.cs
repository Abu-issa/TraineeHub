using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Messaging.Common.Events;

namespace TraineeHub.Messaging.Common.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(SubmissionCreatedEvent message);
    }
}
