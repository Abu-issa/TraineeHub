using System;
using System.Collections.Generic;
using System.Text;

using TraineeHub.Domain.Entities;

namespace TraineeHub.Cli.Infrastructue.DataStore
{
    public class Data
    {
        public List<Trainee> Trainees { get; set; } = new List<Trainee>();
        public List<Topic> Topics { get; set; } = new List<Topic>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Submission> Submissions { get; set; } = new List<Submission>();
    }
}
