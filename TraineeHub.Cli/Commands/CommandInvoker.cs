using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TraineeHub.Cli.Commands
{
    public class CommandInvoker
    {
        private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);
        public void RegisterCommand(ICommand cmd) => _commands[cmd.Name] = cmd;

        public async Task<bool> ExecuteAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return true;
            var parts = Regex.Matches(input, @"[\""].+?[\""]|\S+")
                 .Select(m => m.Value.Trim('"'))
                 .ToArray();
            if (parts.Length == 0) return true;
            var commandName = parts[0];
            var args = parts.Skip(1).ToArray();
            if (parts.Length > 1 && _commands.ContainsKey($"{parts[0]} {parts[1]}"))
            {
                commandName = $"{parts[0]} {parts[1]}";
                args = parts.Skip(2).ToArray();
            }
            if (_commands.TryGetValue(commandName, out var cmd))
            {
                try { return await cmd.Execute(args); }
                catch (Exception ex) { ConsoleHelper.WriteError($"Error: {ex.Message}"); return false; }
            }
            ConsoleHelper.WriteError($"Unknown command: {commandName}");
            ConsoleHelper.WriteInfo("Type 'help' to see available commands");
            return false;
        }

        public IEnumerable<ICommand> GetAllCommands() => _commands.Values;
    }
}

