using System;
using System.Text;
using PipServices.Commons.Convert;

namespace PipServices.Commons.Log
{
    public class ConsoleLogger : Logger
    {
        protected override void Write(LogLevel level, string correlationId, Exception error, string message)
        {
            if (Level < level) return;

            var build = new StringBuilder();
            build.Append('[');
            build.Append(correlationId != null ? correlationId : "---");
            build.Append(':');
            build.Append(level.ToString());
            build.Append(':');
            build.Append(StringConverter.ToString(DateTime.UtcNow));
            build.Append("] ");

            build.Append(message);

            if (error != null)
            {
                if (message.Length == 0)
                    build.Append("Error: ");
                else
                    build.Append(": ");

                build.Append(ComposeError(error));
            }

            var output = build.ToString();

            if (level == LogLevel.Fatal || level == LogLevel.Error || level == LogLevel.Warn)
                Console.Error.WriteLine(output);
            else
                Console.Out.WriteLine(output);
        }
    }
}