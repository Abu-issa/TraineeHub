using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Intreface;
namespace TraineeHub.Application.Services
{
    public class AssignmentServices
    {
        private readonly IAssignmentRepository _repository;
        private readonly ITopicRepository _topicRepository;
        public AssignmentServices(IAssignmentRepository repository , ITopicRepository topicRepository) { 
            _repository = repository;
            this._topicRepository = topicRepository;
        }
        public void AddAssignment(Guid topicId, string titile, string description, int difficulty, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(titile))
            {
                throw new ArgumentException("Title empty.");

            }
            if (difficulty < 1 || difficulty > 5)
            {
                throw new ArgumentException("Difficulty must be between 1 and 5.");

            }
            if (dueDate < DateTime.Now)
            {
                throw new ArgumentException("Due date must be in the future.");
            }
            if (dueDate == default(DateTime))
            {
                throw new ArgumentException("Time must be entered");
            }
            var topicExists = _topicRepository.GetById(topicId);
            if (topicExists == null) {
                throw new ArgumentException("Traineer not found.");
            }
            var assignment = new Assignment();
            assignment.TopicTd = topicId;
            assignment.Title = titile;
            assignment.Description = description;
            assignment.Difficulty = difficulty;
            assignment.DutDate = dueDate;
            _repository.Add(assignment);
        }
        public List<Assignment> AllAssignmentByTopic(Guid topicId)
        {
            return _repository.GetAll().Where(A => A.TopicTd == topicId).ToList();
        }
    }

}