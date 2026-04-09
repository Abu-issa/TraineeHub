using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Cli.Infrastructue.DataStore;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
using TraineeHub.Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        // private readonly JsonDataStore _stor;
        private readonly TraineeHubDbContext _stor;
        public AssignmentRepository(TraineeHubDbContext stor)
        {
            _stor = stor;
            
        }
        public void Add(Assignment assignment)
        {

            try
            { 
                    _stor.Assignments.Add(assignment);
               _stor.SaveChanges();
              
                Console.WriteLine("The data was added and saved successfully.");

            }
            catch (Exception)
            {

                Console.WriteLine("No added");
            }

        }

        public List<Assignment> GetAll()
        {
           // var data = _stor.LoadData();
           //return data.Assignments;
           return _stor.Assignments.ToList();
        }

        public Assignment? GetById(Guid id)
        {
           // var data = _stor.LoadData();
           //return data.Assignments.FirstOrDefault  (x => x.Id == id);
           return _stor.Assignments.FirstOrDefault(x => x.Id == id);
            
        }
        public List<Assignment> GetByTopicId(Guid topicId)
        {
            return _stor.Assignments.Where(a => a.TopicTd == topicId).ToList();
        }
    }
}
