using System;
using PipServices.Commons.Refer;
using System.Collections.Generic;

namespace PipServices.Commons.Log
{
    public sealed class CompositeLogger : Logger, IReferenceable, IDescriptable
    {
        private static readonly Descriptor _locator = new Descriptor("pip-commons", "logger", "composite", "1.0");
        private readonly List<ILogger> _loggers = new List<ILogger>();

        public CompositeLogger()
        {
        }

        public CompositeLogger(IReferences references)
        {
            SetReferences(references);
        }

        public Descriptor GetDescriptor()
        {
            return _locator;
        }

        public void SetReferences(IReferences references)
        {
            var loggers = references.GetOptional(new Descriptor(null, "logger", null, null));
            foreach (var logger in loggers)
            {
                if (logger is ILogger)
                {
                    _loggers.Add((ILogger)logger);
                }
            }
        }

        protected override void Write(LogLevel level, string correlationId, Exception error, string message, params object[] args)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(level, correlationId, error, message, args);
            }
        }
    }
}