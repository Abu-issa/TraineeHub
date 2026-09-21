using TraineeHub.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Cli.Commands
{


    public class TraineeAddCommand : ICommand
    {
        private readonly TraineeServices _service;
        public TraineeAddCommand(TraineeServices service) { 
        _service = service;
        }
        public string Name => "trainee add";
        public async Task<bool> Execute(string[] args)
        {
            if (args.Length < 2 || args.Length>2) {
                ConsoleHelper.WriteError("Usage: trainee add <FullName> <Email>"); 
                return false;
            }
            _service.AddTrainee(args[0], args[1]);
            ConsoleHelper.WriteSuccess($"Trainee {args[0]} added!"); return true;
        }
    }
    public class TraineeListCommand : ICommand
    {
        private readonly TraineeServices _service;
        public TraineeListCommand(TraineeServices service) => _service = service;
        public string Name => "trainee list";
        public async Task<bool> Execute(string[] args)
        {
            var list = _service.AllTrainee();
            if (!list.Any()) ConsoleHelper.WriteInfo("No trainees.");
            else foreach (var t in list) Console.WriteLine($"- {t.Id}|{t.FullName}|{t.Email}");
            return true;
        }
    }
}
