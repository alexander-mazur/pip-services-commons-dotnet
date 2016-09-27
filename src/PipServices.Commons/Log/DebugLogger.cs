using System;
using System.Text;

namespace PipServices.Commons.Log
{
    public sealed class DebugLogger : Logger
    {
        protected override void PerformWrite(string correlationId, LogLevel level, Exception error, string message)
        {
            var build = new StringBuilder();
            build.Append(correlationId ?? "---");
            build.Append(" : ");

            build.Append(message);

            if (error != null)
            {
                if (string.IsNullOrWhiteSpace(message))
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
