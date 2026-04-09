using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Domain.Entities

{
    public class Topic
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Assignment> Assignments { get; set; } = new();

    }
}
