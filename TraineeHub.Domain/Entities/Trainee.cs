using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Domain.Entities

{
    public class Trainee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatAt { get; set; } = DateTime.UtcNow;
        public List<Submission> Submissions { get; set; } = new ();
    }
}
