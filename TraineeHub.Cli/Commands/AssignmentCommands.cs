using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Appliction.Services;

namespace TraineeHub.Cli.Commands
{
    public class AssignmentAddCommand : ICommand
    {
        private readonly AssignmentServices _service;

        public AssignmentAddCommand(AssignmentServices service)
        {
            _service = service;
        }

        public string Name => "assignment add";

        public async Task<bool> Execute(string[] args)
        {
            if (args.Length <1)
            {
                ConsoleHelper.WriteError("Usage: assignment add <Title>  --topic <topicId>  --difficulty [difficulty <1-5>]  --description [description ] --duedate [duedate <yyyy-MM-dd>]");
                return false;
            }

        
            var title = args[0];
            Guid topicId = Guid.Empty;
            int difficulty = 3;

            string description = "No Description";
            
            DateTime dueDate = DateTime.Now.AddDays(7);
          

            
            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--topic":
                        if (i + 1 >= args.Length || !Guid.TryParse(args[i + 1], out topicId))
                        {
                            ConsoleHelper.WriteError("Invalid or missing TopicId. Use GUID format.");
                            return false;
                        }
                        i++;
                        break;

                    case "--description":
                        if (i + 1 >= args.Length)
                        {
                            ConsoleHelper.WriteError("Missing description value.");
                            return false;
                        }
                        description = args[i + 1];
                        i++;
                        break;

                    case "--difficulty":
                        if (i + 1 >= args.Length || !int.TryParse(args[i + 1], out difficulty) || difficulty < 1 || difficulty > 5)
                        {
                            ConsoleHelper.WriteError("Difficulty must be an integer between 1 and 5.");
                            return false;
                        }
                        i++;
                        break;

                    case "--duedate":
                        if (i + 1 >= args.Length || !DateTime.TryParseExact(args[i + 1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
                        {
                            ConsoleHelper.WriteError("Invalid due date. Use format yyyy-MM-dd");
                            return false;
                        }
                        i++;
                        break;

                    default:
                        ConsoleHelper.WriteError("inVaild commands");
                        return false;
                }
            }

            if (topicId == Guid.Empty)
            {
                ConsoleHelper.WriteError("TopicId is required. Use --topic <topicId>");
                return false;
            }

            try
            {
                _service.AddAssignment(topicId, title, description, difficulty, dueDate);
                ConsoleHelper.WriteSuccess($"Assignment '{title}'  | Description:  '{description}'|Diffiulty:  '{difficulty}| DueDate: '{dueDate}' ");
                return true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Error: {ex.Message}");
                return false;
            }
        }
    }

    public class AssignmentListCommand : ICommand
    {
        private readonly AssignmentServices _service;
        public AssignmentListCommand(AssignmentServices service)
        {
            _service = service;
        }
        public string Name => "assignment list";
        public async Task<bool> Execute(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleHelper.WriteError("Usage: assignment list <topicId>");
                return false;
            }
            if (!Guid.TryParse(args[0], out Guid topicId))
            {
                ConsoleHelper.WriteError("Invalid TopicId. Use GUID format.");
                return false;
            }
            var assignments = _service.AllAssignmentByTopic(topicId);
            if (assignments.Count == 0)
            {
                ConsoleHelper.WriteInfo("No assignments found for this topic.");
                return true;
            }
            foreach (var a in assignments)
            {
                Console.WriteLine($"-ID: {a.Id} | Title: {a.Title} | Difficulty: {a.Difficulty} | Due: {a.DutDate:yyyy-MM-dd}");
                Console.WriteLine($"  Description: {a.Description}");
            }
            return true;
        }
    }
}