using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
namespace TraineeHub.Application.Services
{
    public class TopicServices
    { private readonly ITopicRepository _repository;
        public TopicServices(ITopicRepository repository) { _repository = repository; }
        public void AddTopic(string title, string description)
        { if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Title cannot be null or empty.");
            }
        
            var topic = new Topic();
            topic.Title = title;
            topic.Description = description;
            _repository.Add(topic);
        }
        public List<Topic> AllTopic()
        {
            return _repository.GetAll();
        }
    }
}
