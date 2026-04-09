using System;
using System.Collections.Generic;
using System.Text;

namespace TraineeHub.Cli.Commands
{
    public static class ConsoleHelper
    {
        public static void WriteError(string msg) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(msg); Console.ResetColor(); }
        public static void WriteSuccess(string msg) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(msg); Console.ResetColor(); }
        public static void WriteInfo(string msg) { Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine(msg); Console.ResetColor(); }
    }
}
