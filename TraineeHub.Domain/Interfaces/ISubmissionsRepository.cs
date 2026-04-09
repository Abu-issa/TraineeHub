using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Entities;

namespace TraineeHub.Domain.Intreface
{
   public interface ISubmissionsRepository
    {   public void Add(Submission submission);
        public List<Submission> GetAll();
        Submission? GetById(Guid id);
        public void SaveAll();
        void Update(Submission submission);
    }
}
