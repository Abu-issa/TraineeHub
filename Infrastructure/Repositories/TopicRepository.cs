using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Cli.Infrastructue.DataStore;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
using TraineeHub.Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
       // private readonly JsonDataStore _stor;
        private readonly TraineeHubDbContext _stor;

        public TopicRepository(TraineeHubDbContext stor)
        {
            _stor = stor;
            
        }
        public void Add(Topic topic)
        {

            try { 
            //{ var data = _stor.LoadData(); 
            //    data.Topics.Add(topic);
            //    _stor.Save(data);
                _stor.Topics.Add(topic);
                _stor.SaveChanges();
                Console.WriteLine("The data was added and saved successfully.");

            }
            catch (Exception)
            {

                Console.WriteLine("No added");
            }

        }

        public List<Topic> GetAll()
        {
            // var    data = _stor.LoadData();
           
            //return data.Topics;
            return _stor.Topics.ToList();
        }

        public Topic? GetById(Guid id) { 
        //{ var  data = _stor.LoadData();
        //    return data.Topics.FirstOrDefault(t => t.Id == id);
            return _stor.Topics.FirstOrDefault(t => t.Id == id);    
        }
    }
}
