using System;
using PipServices.Commons.Config;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public sealed class CompositeLogger : Logger
    {
        //private readonly AppInsightsLogger _appInsightsLogger = new AppInsightsLogger();
        private readonly DebugLogger _debugLogger = new DebugLogger();
        private readonly EventLogger _eventLogger = new EventLogger();

        public CompositeLogger(IReferences references = null)
            : base(references)
        {
            if (references != null)
                SetReferences(references);
        }

        public new void SetReferences(IReferences references)
        {
            _debugLogger.SetReferences(references);
            _eventLogger.SetReferences(references);
            //_appInsightsLogger.SetReferences(references);
        }

        public override void Configure(ConfigParams config)
        {
            base.Configure(config);

            _debugLogger.Configure(config);
            _eventLogger.Configure(config);
            //_appInsightsLogger.Configure(config);
        }

        protected override void PerformWrite(string correlationId, LogLevel level, Exception error, string message)
        {
            _debugLogger.Write(correlationId, level, error, message);
            _eventLogger.Write(correlationId, level, error, message);
            //_appInsightsLogger.Write(correlationId, level, error, message);
        }
    }
}