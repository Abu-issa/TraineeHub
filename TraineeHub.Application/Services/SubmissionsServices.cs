using System;
using System.Collections.Generic;
using System.Text;

using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;
using TraineeHub.Domain.Intreface;
namespace Appliction.Services
{
  public class SubmissionsServices
    {
        private readonly ISubmissionsRepository _repository;
        private readonly IAssignmentRepository _assignmentsRepository;
        private readonly ITraineeRepositroy _traineeRepository;

        public SubmissionsServices(ISubmissionsRepository repository,IAssignmentRepository assignmentRepository,ITraineeRepositroy traineeRepositroy)
        {
            this._repository = repository;
            this._assignmentsRepository = assignmentRepository;
            this._traineeRepository = traineeRepositroy;
        }

        public void AddSubmission(Guid assignmentId, Guid traineerId, string notes)
        { if (assignmentId == Guid.Empty)
            {
                throw new ArgumentException("Assignment ID cannot be empty.");
            }
        var assignmentExists = _assignmentsRepository.GetById(assignmentId);
            if (assignmentExists == null)
            {
                throw new ArgumentException("Assignment not found.");
            }
            if (traineerId == Guid.Empty)
            {
                throw new ArgumentException("Traineer ID cannot be empty.");
            }
            var traineerExists = _traineeRepository.GetById(traineerId);
            if (traineerExists == null)
            {
                throw new ArgumentException("Traineer not found.");
            }

            var Submission = new Submission();
            Submission.AssignmentId = assignmentId;
            Submission.TraineerId = traineerId;
            Submission.Notes = notes;
            Submission.Stauts = SubmissionStatus.Submitted;
            _repository.Add(Submission);
        }
        public List<Submission> All(Guid traineeTd)
        {
            return _repository.GetAll().Where(t => t.TraineerId == traineeTd).ToList();
        }

        public void Status(Guid submissionId, SubmissionStatus status)
        {
            
            
            var submission = _repository.GetById(submissionId);
            if (submission == null)
            {
                throw new ArgumentException("Submission not found.");
            }
            submission.Stauts = status;

            _repository.SaveAll();

        }
       
    }

    
}
