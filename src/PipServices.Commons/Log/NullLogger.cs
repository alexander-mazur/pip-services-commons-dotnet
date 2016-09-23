using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public sealed class NullLogger : AbstractLogger
    {
        static NullLogger()
        {
            Descriptor = new Descriptor("pip-commons", "logger", "null", "1.0");
        }

        protected override void PerformWrite(string correlationId, LogLevel level, Exception error, string message)
        {
        }
    }
}