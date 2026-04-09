using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Cli.Commands
{
    public interface ICommand
    {
        string Name { get; }
        Task<bool> Execute(string[] args);
    }
}
