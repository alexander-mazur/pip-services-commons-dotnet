using System;

namespace PipServices.Commons.Log
{
    public interface ILogger
    {
        LogLevel Level { get; set; }

        void Write(string correlationId, LogLevel level, Exception error, string message, params object[] args);

        void Fatal(string correlationId, string message, params object[] args);
        void Fatal(string correlationId, Exception error);
        void Fatal(string correlationId, Exception error, string message, params object[] args);

        void Error(string correlationId, string message, params object[] args);
        void Error(string correlationId, Exception error);
        void Error(string correlationId, Exception error, string message, params object[] args);

        void Warn(string correlationId, string message, params object[] args);

        void Info(string correlationId, string message, params object[] args);

        void Debug(string correlationId, string message, params object[] args);

        void Trace(string correlationId, string message, params object[] args);
    }
}
