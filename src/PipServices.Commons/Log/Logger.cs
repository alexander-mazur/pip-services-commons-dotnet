using System;
using System.Text;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public abstract class Logger : IReferenceable, IDescriptable, ILogger, IConfigurable
    {
        protected static Descriptor Descriptor;

        protected Logger(IReferences references = null)
        {
            if (references != null)
                SetReferences(references);

            Level = LogLevel.Trace;
        }

        protected IReferences References { get; set; }

        public virtual void Configure(ConfigParams config)
        {
            Level = config.GetAsEnum<LogLevel>("Log.Level");
        }

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public LogLevel Level { get; set; }

        public virtual void Write(string correlationId, LogLevel level, Exception error, string message,
            params object[] args)
        {
            if (level <= Level)
            {
                message = ComposeMessage(message, args);
                PerformWrite(correlationId, level, error, message);
            }
        }

        public void Fatal(string correlationId, string message, params object[] args)
        {
            Fatal(correlationId, null, message, args);
        }

        public void Fatal(string correlationId, Exception error)
        {
            Fatal(correlationId, error, null);
        }

        public void Fatal(string correlationId, Exception error, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Fatal, error, message, args);
        }

        public void Error(string correlationId, string message, params object[] args)
        {
            Error(correlationId, null, message, args);
        }

        public void Error(string correlationId, Exception error)
        {
            Error(correlationId, error, null);
        }

        public void Error(string correlationId, Exception error, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Error, error, message, args);
        }

        public void Warn(string correlationId, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Warn, null, message, args);
        }

        public void Info(string correlationId, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Info, null, message, args);
        }

        public void Debug(string correlationId, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Debug, null, message, args);
        }

        public void Trace(string correlationId, string message, params object[] args)
        {
            Write(correlationId, LogLevel.Trace, null, message, args);
        }

        public void SetReferences(IReferences references)
        {
            References = references;
        }

        protected LogLevel ParseLogLevel(object newLevel)
        {
            if (newLevel == null) return Level;

            var value = newLevel.ToString().ToUpper();
            if (value == "0" || value == "NONE")
                return LogLevel.Nothing;
            if (value == "1" || value == "FATAL")
                return LogLevel.Fatal;
            if (value == "2" || value == "ERROR")
                return LogLevel.Error;
            if (value == "3" || value == "WARN")
                return LogLevel.Warn;
            if (value == "4" || value == "INFO")
                return LogLevel.Info;
            if (value == "5" || value == "DEBUG")
                return LogLevel.Debug;
            if (value == "6" || value == "TRACE")
                return LogLevel.Trace;

            return Level;
        }

        protected abstract void PerformWrite(string correlationId, LogLevel level, Exception error, string message);

        protected string ComposeError(Exception error)
        {
            var builder = new StringBuilder();

            while (error != null)
            {
                if (builder.Length > 0)
                    builder.Append(" Caused by error: ");

                builder.Append(error.Message).Append(" StackTrace: ").Append(error.StackTrace);

                error = error.InnerException;
            }

            return builder.ToString();
        }

        protected string ComposeMessage(string message, params object[] args)
        {
            var build = new StringBuilder();

            if (args.Length > 0)
                build.AppendFormat(message ?? "", args);
            else
                build.Append(message ?? "");

            return build.ToString();
        }
    }
}
