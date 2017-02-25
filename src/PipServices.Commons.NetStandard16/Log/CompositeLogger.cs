using System;
using PipServices.Commons.Refer;
using System.Collections.Generic;

namespace PipServices.Commons.Log
{
    /// <summary>
    /// Class CompositeLogger.
    /// </summary>
    /// <seealso cref="PipServices.Commons.Log.Logger" />
    /// <seealso cref="PipServices.Commons.Refer.IReferenceable" />
    public class CompositeLogger : Logger, IReferenceable
    {
        protected readonly List<ILogger> _loggers = new List<ILogger>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeLogger"/> class.
        /// </summary>
        /// <param name="references">The references.</param>
        public CompositeLogger(IReferences references = null)
        {
            Level = LogLevel.Trace;

            if (references != null)
                SetReferences(references);
        }

        /// <summary>
        /// Sets the references.
        /// </summary>
        /// <param name="references">The references.</param>
        public void SetReferences(IReferences references)
        {
            var loggers = references.GetOptional<ILogger>(new Descriptor(null, "logger", null, null, null));
            foreach (var logger in loggers)
            {
                if (logger != this)
                    _loggers.Add(logger);
            }
        }

        /// <summary>
        /// Writes the specified level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        protected override void Write(LogLevel level, string correlationId, Exception error, string message)
        {
            foreach (var logger in _loggers)
                logger.Log(level, correlationId, error, message);
        }
    }
}