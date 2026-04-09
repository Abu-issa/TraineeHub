using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Enum;

namespace TraineeHub.Domain.Entities

{
    public class Submission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssignmentId { get; set; }
        public Guid TraineerId { get; set; }

        public string Notes { get; set; } = string.Empty;

        

        public SubmissionStatus Stauts { get; set; } = SubmissionStatus.Pending;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public Assignment Assignment { get; set; } = null!;
        public Trainee Trainee { get; set; }=null!;
    }
}
