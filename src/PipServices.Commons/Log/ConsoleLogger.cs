using System;
using PipServices.Commons.Refer;
using System.Text;

namespace PipServices.Commons.Log
{
    public class ConsoleLogger : Logger, IDescriptable
    {
        private static Descriptor _locator = new Descriptor("pip-commons", "logger", "console", "1.0");

        public Descriptor GetDescriptor()
        {
            return _locator;
        }

        protected string ComposeError(Exception error)
        {
            var builder = new StringBuilder();

            while (error != null)
            {
                if (builder.Length > 0)
                    builder.Append(" Caused by error: ");

                builder.Append(error.Message)
                    .Append(" StackTrace: ")
                    .Append(error.StackTrace);
            }

            return builder.ToString();
        }

        protected string ComposeMessage(string message, object[] args)
        {
            message = message != null ? message : "";
            if (args != null && args.Length > 0)
            {
                message = string.Format(message, args);
            }
            return message;
        }

        protected override void Write(LogLevel level, string correlationId, Exception error, string message, params object[] args)
        {
            if (Level < level) return;

            var build = new StringBuilder();
            build.Append('[');
            build.Append(correlationId != null ? correlationId : "---");
            build.Append(':');
            build.Append(Level.ToString());
            build.Append(':');
            build.Append(DateTime.UtcNow);
            build.Append("] ");

            message = ComposeMessage(message, args);
            build.Append(message);

            if (error != null)
            {
                if (message.Length == 0)
                {
                    build.Append("Error: ");
                }
                else
                {
                    build.Append(": ");
                }

                build.Append(ComposeError(error));
            }

            var output = build.ToString();

            if (level == LogLevel.Fatal || level == LogLevel.Error || level == LogLevel.Warn)
            {
                Console.Error.WriteLine(output);
            }
            else
            {
                Console.Out.WriteLine(output);
            }
        }
    }
}