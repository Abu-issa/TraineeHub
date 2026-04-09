using Appliction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;

namespace TraineeHub.Cli.Commands
{
    public class SubmissionAddCommand : ICommand
    {
        private readonly SubmissionsServices _submissionsServices;

        public SubmissionAddCommand(SubmissionsServices submissionsServices)
        {
            _submissionsServices = submissionsServices;
        }

        public string Name => "submission add";

        public async Task<bool> Execute(string[] args)
        {
            if (args.Length != 3)
            {
                ConsoleHelper.WriteError(
                    "Usage: submission add <traineeId> <assignmentId> <notes>"
                );
                return false;
            }

            if (!Guid.TryParse(args[0], out var traineeId))
            {
                ConsoleHelper.WriteError("Invalid Trainee ID format.");
                return false;
            }

            if (!Guid.TryParse(args[1], out var assignmentId))
            {
                ConsoleHelper.WriteError("Invalid Assignment ID format.");
                return false;
            }

            _submissionsServices.AddSubmission(
                assignmentId,
                traineeId,
                
                args[2]
            );

            ConsoleHelper.WriteSuccess("Submission added successfully.");
            return true;
        }
    }
    public class SubmissionListCommand : ICommand
    {
        private readonly SubmissionsServices _service;
        public SubmissionListCommand(SubmissionsServices service)
        {
            _service = service;
        }
        public string Name => "submission list";
        public async Task<bool> Execute(string[] args)
        {
            if (args.Length != 1)
            {
                ConsoleHelper.WriteError("Usage: submission list <traineeId>");
                return false;
            }
            if (!Guid.TryParse(args[0], out Guid traineeId))
            {
                ConsoleHelper.WriteError("Invalid TraineeId. Use GUID format.");
                return false;
            }
            var submission = _service.All(traineeId);
            if (submission.Count == 0)
            {
                ConsoleHelper.WriteInfo("No submission found for this traineeId.");
                return true;
            }
            foreach (var a in submission)
            {
                Console.WriteLine($"ID: {a.Id} |TraineerId: {a.TraineerId} |AssignmentId: {a.AssignmentId} | Notes:  {a.Notes} | Stauts: {a.Stauts}  | SubmittedAt: {a.SubmittedAt}");
            }
            return true;
        }
    }
    public class SubmissionApprove : ICommand
    {
        private readonly SubmissionsServices _submissionsServices;
      
        public SubmissionApprove(SubmissionsServices submissionsServices)
        {
            _submissionsServices = submissionsServices;
           

        }   
        public string Name => "submission approve";

        public async Task<bool> Execute(string[] args)
        {
           if(args.Length !=1)
            {
                ConsoleHelper.WriteError("Usage: submission approve <submissionId>");
                return false;
            }
           if(!Guid.TryParse(args[0], out var submissionId))
            {
                ConsoleHelper.WriteError("Invalid Submission ID format.");
                return false;
            }
            _submissionsServices.Status(submissionId, SubmissionStatus.Approve);
            ConsoleHelper.WriteSuccess("Submission approved successfully.");
            return true;
        }
    }
    public class SubmissionRejected : ICommand
    {
        private readonly SubmissionsServices _submissionsServices;

        public SubmissionRejected(SubmissionsServices submissionsServices)
        {
            _submissionsServices = submissionsServices;


        }
        public string Name => "submission Rejected";

        public async Task<bool> Execute(string[] args)
        {
            if (args.Length != 1)
            {
                ConsoleHelper.WriteError("Usage: submission Rejected <submissionId>");
                return false;
            }
            if (!Guid.TryParse(args[0], out var submissionId))
            {
                ConsoleHelper.WriteError("Invalid Submission ID format.");
                return false;
            }
            _submissionsServices.Status(submissionId, SubmissionStatus.Rejected);
            ConsoleHelper.WriteSuccess("Submission Rejected successfully.");
            return true;
        }
    }
}
