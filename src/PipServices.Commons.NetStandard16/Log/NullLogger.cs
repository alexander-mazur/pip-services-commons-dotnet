using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public sealed class NullLogger : ILogger, IDescriptable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-commons", "logger", "null", "1.0");

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public LogLevel Level
        {
            get { return LogLevel.None; }
            set { }
        }

        public void Debug(string correlationId, string message, params object[] args)
        {
        }

        public void Debug(string correlationId, Exception error, string message = null, params object[] args)
        {
        }

        public void Error(string correlationId, string message, params object[] args)
        {
        }

        public void Error(string correlationId, Exception error, string message = null, params object[] args)
        {
        }

        public void Fatal(string correlationId, string message, params object[] args)
        {
        }

        public void Fatal(string correlationId, Exception error, string message = null, params object[] args)
        {
        }

        public void Info(string correlationId, string message, params object[] args)
        {
        }

        public void Info(string correlationId, Exception error, string message = null, params object[] args)
        {
        }

        public void Log(LogLevel level, string correlationId, Exception error, string message, params object[] args)
        {
        }

        public void Trace(string correlationId, string message, params object[] args)
        {
        }

        public void Trace(string correlationId, Exception error, string message = null, params object[] args)
        {
        }

        public void Warn(string correlationId, string message, params object[] args)
        {
        }

        public void Warn(string correlationId, Exception error, string message = null, params object[] args)
        {
        }
    }
}