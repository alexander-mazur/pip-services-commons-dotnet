using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public sealed class ConsoleLogger : Logger
    {
        static ConsoleLogger()
        {
            Descriptor = new Descriptor("pip-commons", "logger", "console", "1.0");
        }

        protected override void PerformWrite(string correlationId, LogLevel level, Exception error, string message)
        {
            if (Level < level) return;

            ////var output = LogFormatter.Format(level, message);
            //if (correlationId != null)
            //    output += ", correlated to " + correlationId;

            //if (level >= LogLevel.Fatal && level <= LogLevel.Warn)
            //    Console.Error.WriteLine(output);
            //else Console.Out.WriteLine(output);
        }
    }
}