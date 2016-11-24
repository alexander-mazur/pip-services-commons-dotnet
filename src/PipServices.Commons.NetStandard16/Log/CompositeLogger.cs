using System;
using PipServices.Commons.Refer;
using System.Collections.Generic;

namespace PipServices.Commons.Log
{
    public class CompositeLogger : Logger, IReferenceable, IDescriptable
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "logger", "composite", "default", "1.0");

        protected readonly List<ILogger> _loggers = new List<ILogger>();

        public CompositeLogger() { }

        public CompositeLogger(IReferences references)
        {
            SetReferences(references);
        }

        public virtual Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public virtual void SetReferences(IReferences references)
        {
            var loggers = references.GetOptional<ILogger>(new Descriptor(null, "logger", null, null, null));
            _loggers.AddRange(loggers);
        }

        protected override void Write(LogLevel level, string correlationId, Exception error, string message)
        {
            foreach (var logger in _loggers)
                logger.Log(level, correlationId, error, message);
        }
    }
}