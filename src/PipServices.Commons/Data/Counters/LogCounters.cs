using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;
using PipServices.Commons.Log;

namespace PipServices.Commons.Counters
{
    public class LogCounters : CachedCounters, IDescriptable, IReferenceable
    {
        private static readonly Descriptor _descriptor = new Descriptor("pip-commons", "counters", "log", "1.0");
        private ILogger _logger;

        public void SetReferences(IReferences references)
        {
            var logger = references.GetOptional(new Descriptor(null, "logger", null, null));
            if(logger is ILogger)
            {
                _logger = (ILogger)logger;
            }
        }

        private string CounterToString(Counter counter)
        {
            var result = "Counter " + counter.Name;
            //result += " Type: " + (int)counter.Type;
            if (counter.Last.HasValue)
                result += " Last: " + counter.Last.Value;
            if (counter.Count.HasValue)
                result += " Count: " + counter.Count.Value;
            if (counter.Min.HasValue)
                result += " Min: " + counter.Min.Value;
            if (counter.Max.HasValue)
                result += " Max: " + counter.Max.Value;
            if (counter.Avg.HasValue)
                result += " Average: " + counter.Avg.Value;
            if (counter.Time.HasValue)
                result += " Time: " + counter.Time.Value.ToString("s");
            return result;
        }

        protected override void Save(List<Counter> counters)
        {
            if (_logger == null) return;
            if (counters.Count == 0) return;

            foreach (var counter in counters)
            {
                _logger.Debug(null, CounterToString(counter));
            }
        }

        public Descriptor GetDescriptor()
        {
            return _descriptor;
        }
    }
}
