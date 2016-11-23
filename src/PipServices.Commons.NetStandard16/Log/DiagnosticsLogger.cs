using PipServices.Commons.Refer;
using System;
using System.Text;

namespace PipServices.Commons.Log
{
    public class DiagnosticsLogger : Logger, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-commons", "logger", "diagnostics", "1.0");

        public virtual Descriptor GetDescriptor()
        {
            return Descriptor;
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

        protected override void Write(LogLevel level, string correlationId, Exception error, string message)
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

            switch (level)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    System.Diagnostics.Trace.TraceError(output);
                    break;
                case LogLevel.Warn:
                    System.Diagnostics.Trace.TraceWarning(output);
                    break;
                case LogLevel.Info:
                    System.Diagnostics.Trace.TraceInformation(output);
                    break;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    System.Diagnostics.Debug.WriteLine(output);
                    break;
            }
        }
    }
}
