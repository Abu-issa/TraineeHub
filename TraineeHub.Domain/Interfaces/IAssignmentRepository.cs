using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Entities;
namespace TraineeHub.Domain.Intreface
{
  public interface IAssignmentRepository
     {
         public void Add(Assignment assignment);
         public List<Assignment> GetAll();
         Assignment? GetById(Guid id);
        List<Assignment> GetByTopicId(Guid topicId);

    }
}
