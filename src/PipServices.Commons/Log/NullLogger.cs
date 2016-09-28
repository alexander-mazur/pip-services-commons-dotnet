using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public sealed class NullLogger : ILogger, IDescriptable
    {
        private static Descriptor _locator = new Descriptor("pip-commons", "logger", "null", "1.0");

        public Descriptor GetDescriptor()
        {
            return _locator;
        }


        public LogLevel Level
        {
            get { return LogLevel.Nothing; }
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