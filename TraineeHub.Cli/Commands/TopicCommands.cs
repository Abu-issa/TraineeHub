using Appliction.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Cli.Commands
{
    public class TopicAddCommand : ICommand
    {
        private readonly TopicServices topicServices;
        public TopicAddCommand(TopicServices topicServices)
        {
            this.topicServices = topicServices;
        }
        public string Name => "topic add";


        public async Task<bool> Execute(string[] args)
        {
            if (args.Length < 1)
            {
                ConsoleHelper.WriteError("Usage: topic add <Title> [Description]");
                return false;
            }
            var title = args[0];
            var description = "";
            if(args.Length >1)
                description = args[1];
            topicServices.AddTopic(title, description);
            ConsoleHelper.WriteSuccess($"Topic {args[0]} added!");
            return true;
        }
    }
    public class TopicListCommand : ICommand
    {
        private readonly TopicServices topicServices;
        public TopicListCommand(TopicServices topicServices)
        {
            this.topicServices = topicServices;
        }
        public string Name => "topic list";
        public async Task<bool> Execute(string[] args)
        {
            var list = topicServices.AllTopic();
            if (!list.Any()) ConsoleHelper.WriteInfo("No topics.");
            else foreach (var t in list) Console.WriteLine($"- {t.Id}|{t.Title}|{t.Description}");
            return true;
        }
    }
}