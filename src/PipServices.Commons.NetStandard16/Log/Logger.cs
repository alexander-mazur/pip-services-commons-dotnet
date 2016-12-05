using System;
using PipServices.Commons.Config;

namespace PipServices.Commons.Log
{
    public abstract class Logger : ILogger, IReconfigurable
    {
        public Logger()
        {
            Level = LogLevel.Info;
        }

        public LogLevel Level { get; set; }

        public virtual void Configure(ConfigParams config)
        {
            Level = LogLevelConverter.ToLogLevel(
                config.GetAsObject("level") ?? Level
            );
        }

        protected abstract void Write(LogLevel level, string correlationId, Exception error, string message);

        protected void FormatAndWrite(LogLevel level, string correlationId, Exception error, string message, object[] args)
        {
            message = message != null ? message : "";
            if (args != null && args.Length > 0)
                message = string.Format(message, args);

            Write(level, correlationId, error, message);
        }

        public void Log(LogLevel level, string correlationId, Exception error, string message, params object[] args)
        {
            FormatAndWrite(level, correlationId, error, message, args);
        }

        public void Fatal(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Fatal, correlationId, null, message, args);
        }

        public void Fatal(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Fatal, correlationId, error, message, args);
        }

        public void Error(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Error, correlationId, null, message, args);
        }

        public void Error(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Error, correlationId, error, message, args);
        }

        public void Warn(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Warn, correlationId, null, message, args);
        }

        public void Warn(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Warn, correlationId, error, message, args);
        }

        public void Info(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Info, correlationId, null, message, args);
        }

        public void Info(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Info, correlationId, error, message, args);
        }

        public void Debug(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Debug, correlationId, null, message, args);
        }

        public void Debug(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Debug, correlationId, error, message, args);
        }

        public void Trace(string correlationId, string message, params object[] args)
        {
            FormatAndWrite(LogLevel.Trace, correlationId, null, message, args);
        }

        public void Trace(string correlationId, Exception error, string message = null, params object[] args)
        {
            FormatAndWrite(LogLevel.Trace, correlationId, error, message, args);
        }
    }
}