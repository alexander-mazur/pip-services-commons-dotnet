using System;
using PipServices.Commons.Config;

namespace PipServices.Commons.Log
{
    public abstract class Logger : ILogger, IReconfigurable
    {
        public LogLevel Level { get; set; }

        public virtual void Configure(ConfigParams config)
        {
            Level = config.GetAsEnum<LogLevel>("level");
        }

        protected abstract void Write(LogLevel level, string correlationId, Exception error, string message, params object[] args);

        public void Log(LogLevel level, string correlationId, Exception error, string message, params object[] args)
        {
            Write(level, correlationId, error, message, args);
        }

        public void Fatal(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Fatal, correlationId, null, message, args);
        }

        public void Fatal(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Fatal, correlationId, error, message, args);
        }

        public void Error(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Error, correlationId, null, message, args);
        }

        public void Error(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Error, correlationId, error, message, args);
        }

        public void Warn(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Warn, correlationId, null, message, args);
        }

        public void Warn(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Warn, correlationId, error, message, args);
        }

        public void Info(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Info, correlationId, null, message, args);
        }

        public void Info(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Info, correlationId, error, message, args);
        }

        public void Debug(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Debug, correlationId, null, message, args);
        }

        public void Debug(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Debug, correlationId, error, message, args);
        }

        public void Trace(string correlationId, string message, params object[] args)
        {
            Write(LogLevel.Trace, correlationId, null, message, args);
        }

        public void Trace(string correlationId, Exception error, string message = null, params object[] args)
        {
            Write(LogLevel.Trace, correlationId, error, message, args);
        }
    }
}