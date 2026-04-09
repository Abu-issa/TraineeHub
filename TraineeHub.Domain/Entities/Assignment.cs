using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Domain.Entities

{
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TopicTd { get; set; }// no value because  assigned to topic
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Difficulty { get; set; } // 1 to 5
        public DateTime DutDate { get; set; }
        public List<Submission> Submissions { get; set; } = new();
        public Topic Topic { get; set; } = null!;
    }
}
