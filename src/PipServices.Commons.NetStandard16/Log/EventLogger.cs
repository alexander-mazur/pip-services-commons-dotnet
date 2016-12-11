using PipServices.Commons.Refer;
using System;

namespace PipServices.Commons.Log
{
    public class EventLogger : Logger, IDescriptable
    {
        public static Descriptor Descriptor = new Descriptor("pip-services-commons", "logger", "event", "default", "1.0");

        public virtual Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        protected override void Write(LogLevel level, string correlationId, Exception error, string message)
        {

            if (error != null)
            {
                if (string.IsNullOrWhiteSpace(message))
                    message = "Error: " + ComposeError(error);
                else
                    message = message + ": " + ComposeError(error);
            }

            switch (level)
            {
                case LogLevel.Fatal:
                    LogEventSource.Current.Fatal(correlationId, message);
                    break;
                case LogLevel.Error:
                    LogEventSource.Current.Error(correlationId, message);
                    break;
                case LogLevel.Warn:
                    LogEventSource.Current.Warn(correlationId, message);
                    break;
                case LogLevel.Info:
                    LogEventSource.Current.Info(correlationId, message);
                    break;
                case LogLevel.Debug:
                    LogEventSource.Current.Debug(correlationId, message);
                    break;
                case LogLevel.Trace:
                    LogEventSource.Current.Trace(correlationId, message);
                    break;
            }
        }
    }
}
