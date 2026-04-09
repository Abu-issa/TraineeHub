using Appliction.Services;
using Infrastructure.Repositories;
using TraineeHub.Cli.Commands;
using TraineeHub.Cli.Infrastructue.DataStore;
using TraineeHub.Domain.Entities;
using TraineeHub.Domain.Enum;
using TraineeHub.Domain.Intreface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TraineeHub.Infrastructure.Persistence;


namespace TraineeHub.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)

        {
            var services = new ServiceCollection();

            // services.AddSingleton<JsonDataStore>();
            services.AddDbContext<TraineeHubDbContext>(options =>
     options.UseSqlServer(
         "Server=(localdb)\\MSSQLLocalDB;Database=TraineeHubDb02;Trusted_Connection=True;"
     ));

            services.AddScoped<ITraineeRepositroy, TraineeRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<ISubmissionsRepository, SubmissionsRepository>();
            /* services.AddSingleton<ITraineeRepositroy, TraineeRepository>();
             services.AddSingleton<ITopicRepository, TopicRepository>();
             services.AddSingleton<IAssignmentRepository, AssignmentRepository>();
             services.AddSingleton<ISubmissionsRepository, SubmissionsRepository>();
            */
            services.AddScoped<TraineeServices>();
            services.AddScoped<TopicServices>();
            services.AddScoped<AssignmentServices>();
            services.AddScoped<SubmissionsServices>();

            services.AddScoped<CommandInvoker>();

           
            services.AddScoped<ICommand, TraineeAddCommand>();
            services.AddScoped<ICommand, TraineeListCommand>();
            services.AddScoped<ICommand, TopicAddCommand>();
            services.AddScoped<ICommand, TopicListCommand>();
            services.AddScoped<ICommand, AssignmentAddCommand>();
            services.AddScoped<ICommand, AssignmentListCommand>();
            services.AddScoped<ICommand, SubmissionAddCommand>();
            services.AddScoped<ICommand, SubmissionListCommand>();
            services.AddScoped<ICommand, SubmissionApprove>();
            services.AddScoped<ICommand, SubmissionRejected>();

            var serviceProvider = services.BuildServiceProvider();

            var invoker = serviceProvider.GetRequiredService<CommandInvoker>();

            var commands = serviceProvider.GetServices<ICommand>();
            foreach (var cmd in commands)
            {
                invoker.RegisterCommand(cmd);
            }
            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TraineeHubDbContext>();

                db.Database.Migrate();

                DbSeeder.Seed(db);
            }

            Console.WriteLine("Welcome to TraineeHub CLI! Type 'help' for commands.");
            while (true)
            {
                Console.Write(">  ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;
                if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Available Commands:");
                    foreach (var c in invoker.GetAllCommands()) Console.WriteLine($"- {c.Name}");
                    continue;
                }
                await invoker.ExecuteAsync(input);
            }
                //{
                //    var stor = new JsonDataStore();
                //    var traineeRepository = new TraineeRepository(stor);
                //    var topicRepository = new TopicRepository(stor);
                //    var assignmentRepository = new AssignmentRepository(stor);
                //    var submissionRepository = new SubmissionsRepository(stor);

                //    var traineeService = new TraineeServices(traineeRepository);
                //    var topicService = new TopicServices(topicRepository);
                //    var assignmentService = new AssignmentServices(assignmentRepository, topicRepository);
                //    var submissionService = new SubmissionsServices(submissionRepository, assignmentRepository, traineeRepository);
                //    var invoker = new CommandInvoker();

                //    invoker.RegisterCommand(new TraineeAddCommand(traineeService));
                //    invoker.RegisterCommand(new TraineeListCommand(traineeService));
                //    invoker.RegisterCommand(new TopicAddCommand(topicService));
                //    invoker.RegisterCommand(new TopicListCommand(topicService));
                //    invoker.RegisterCommand(new AssignmentAddCommand(assignmentService));
                //    invoker.RegisterCommand(new AssignmentListCommand(assignmentService));
                //    invoker.RegisterCommand(new SubmissionApprove(submissionService));
                //    invoker.RegisterCommand(new SubmissionAddCommand(submissionService));
                //    invoker.RegisterCommand(new SubmissionListCommand(submissionService));
                //    invoker.RegisterCommand(new SubmissionRejected(submissionService));
                //    Console.WriteLine("Welcome to TraineeHub CLI! Type 'help' for commands.");

                //    while (true)
                //    {
                //        Console.Write("> ");
                //        var input = Console.ReadLine()?.Trim();
                //        if (string.IsNullOrEmpty(input)) continue;
                //        if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;
                //        if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                //        {
                //            Console.WriteLine("Available Commands:");
                //            foreach (var c in invoker.GetAllCommands()) Console.WriteLine($"- {c.Name}");
                //            continue;
                //        }
                //        await invoker.ExecuteAsync(input);


                //    var stor = new JsonDataStore();

                //    var traineeRepository = new TraineeRepository(stor) ;
                //   // var traineeRepository = new JsonRepository<Trainee>(stor);
                //    var topicRepository = new TopicRepository(stor) ;
                //    var assignmentRepository = new AssignmentRepository(stor) ; 
                //    var submissionRepository = new SubmissionsRepository(stor) ;

                //    var traineeService = new TraineeServices(traineeRepository);
                //    var topicService = new TopicServices(topicRepository);
                //    var assignmentService = new AssignmentServices(assignmentRepository, traineeRepository);
                //    var submissionService = new SubmissionsServices(submissionRepository, assignmentRepository, traineeRepository);

                //    RunCli(traineeService, topicService, assignmentService, submissionService);


                //}
                //static void RunCli(TraineeServices traineeService, TopicServices topicService, AssignmentServices assignmentService, SubmissionsServices submissionService)
                //{
                //    while (true)
                //    {
                //        Console.WriteLine("1. Add Trainee");
                //        Console.WriteLine("2. List Trainee");
                //        Console.WriteLine("3. Add Topic");
                //        Console.WriteLine("4. List Topic");
                //        Console.WriteLine("5. Add Assignments");
                //        Console.WriteLine("6. List Assignments(Topic-Title-");
                //        Console.WriteLine("7.Submission Add");
                //        Console.WriteLine("8. Submission Approve ");
                //        Console.WriteLine("9. Submission Reject  ");
                //        Console.WriteLine("10. Submission List-Trainee- ");
                //        Console.WriteLine("11. Exit");
                //        var choice = Console.ReadLine();
                //        switch (choice)
                //        {
                //            case "1":
                //                Console.Write("Enter Full Name: ");
                //                var fullName = Console.ReadLine();
                //                Console.Write("Enter Email: ");
                //                var email = Console.ReadLine();

                //                try
                //                {
                //                    traineeService.AddTrainee(fullName, email);
                //                }
                //                catch (ArgumentException e)
                //                {

                //                    Console.WriteLine(e.Message);
                //                }
                //                break;
                //            case "2":
                //                Console.WriteLine("All The List Trainee ");
                //                var all = traineeService.AllTrainee();
                //                foreach (var trainee in all)
                //                {
                //                    Console.WriteLine($"{trainee.Id} | {trainee.FullName} | {trainee.Email}");

                //                }
                //                break;
                //            case "3":
                //                Console.WriteLine("Title");
                //                var title = Console.ReadLine();
                //                Console.WriteLine("Description");
                //                var description = Console.ReadLine();
                //                topicService.AddTopic(title, description);
                //                break;
                //            case "4":
                //                Console.WriteLine("All The List Topic");
                //                var allTopic = topicService.AllTopic();
                //                foreach (var topic in allTopic)
                //                {
                //                    Console.WriteLine($"{topic.Id} | {topic.Title} ");
                //                }

                //                break;
                //            case "5":
                //                Console.Write("Topic Id: ");
                //                var topicId = Guid.Parse(Console.ReadLine()!);

                //                Console.Write("Title: ");
                //                var aTitle = Console.ReadLine()!;

                //                Console.Write("Description: ");
                //                var aDesc = Console.ReadLine()!;

                //                Console.Write("Difficulty (1-5): ");
                //                var difficulty = int.Parse(Console.ReadLine()!);

                //                Console.Write("Due Date (yyyy-MM-dd): ");
                //                var dueDate = DateTime.Parse(Console.ReadLine()!);

                //                try
                //                {
                //                    assignmentService.AddAssignment(topicId, aTitle, aDesc, difficulty, dueDate);
                //                }
                //                catch (ArgumentException e)
                //                {

                //                    Console.WriteLine(e.Message); 
                //                }

                //                break;
                //            case "6":
                //                Console.Write("Topic Id: ");
                //                var id = Guid.Parse(Console.ReadLine()!);

                //                var assignments = assignmentService.AllAssignmentByTopic(id);
                //                foreach (var a in assignments)
                //                    Console.WriteLine($"{a.Id} | {a.Title} | Difficulty: {a.Difficulty}");

                //                break;
                //            case "7":
                //                Console.Write("Assignment Id: ");
                //                var assId = Guid.Parse(Console.ReadLine()!);

                //                Console.Write("Trainee Id: ");
                //                var tId = Guid.Parse(Console.ReadLine()!);

                //                Console.Write("Notes: ");
                //                var notes = Console.ReadLine()!;

                //                submissionService.AddSubmission(assId, tId, notes);


                //                break;
                //            case "8":
                //                Console.Write("Submission Id: ");
                //                var approveId = Guid.Parse(Console.ReadLine()!);

                //                try
                //                {
                //                    submissionService.Status(approveId, SubmissionStatus.Approve);
                //                }
                //                catch (ArgumentException a)
                //                {

                //                    Console.WriteLine(a.Message);
                //                }


                //                break;
                //            case "9":
                //                Console.Write("Submission Id: ");
                //                var rejectId = Guid.Parse(Console.ReadLine()!);

                //                try
                //                {
                //                    submissionService.Status(rejectId, SubmissionStatus.Rejected);
                //                }
                //                catch (ArgumentException z)
                //                {

                //                    Console.WriteLine(z.Message);
                //                }


                //                break;
                //            case "10":
                //                Console.Write("Trainee Id: ");
                //                var id02 = Guid.Parse(Console.ReadLine()!);

                //                var submissions = submissionService.All(id02);
                //                foreach (var s in submissions)
                //                    Console.WriteLine($"{s.Id} | Status: {s.Stauts} | {s.SubmittedAt}");

                //                break;
                //            case "11":
                //                return;

                //            default:
                //                Console.WriteLine("Invalid choice.");
                //                break;
                //        }
                //    }
                //}
            }
    }
}