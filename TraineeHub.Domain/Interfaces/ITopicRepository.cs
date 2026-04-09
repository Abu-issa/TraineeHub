using System;
using System.Collections.Generic;
using System.Text;
   using TraineeHub.Domain.Entities;

namespace TraineeHub.Domain.Intreface
{
    public interface ITopicRepository
    {
        public void Add(Topic topic);
        public List<Topic> GetAll();
        Topic? GetById(Guid id);    
    }
}
