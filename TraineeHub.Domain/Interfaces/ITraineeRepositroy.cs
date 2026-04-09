using System;
using System.Collections.Generic;
using System.Text;

using TraineeHub.Domain.Entities;

namespace TraineeHub.Domain.Intreface
{
    public interface ITraineeRepositroy
    {
        public void Add(Trainee trainee);
        public List<Trainee> GetAll();

        Trainee? GetById(Guid id);
        Trainee? GetByEmail(string email);
    }
}
