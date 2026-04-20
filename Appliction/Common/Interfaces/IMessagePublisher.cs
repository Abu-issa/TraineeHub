using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Appliction.Common.Events;

namespace TraineeHub.Appliction.Common.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(SubmissionCreatedEvent message);
    }
}
