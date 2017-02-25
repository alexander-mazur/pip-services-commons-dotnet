using System;
using System.Collections.Generic;
using System.Linq;
using PipServices.Commons.Convert;
using PipServices.Commons.Log;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public class LogCounters : CachedCounters, IReferenceable
    {
        private readonly CompositeLogger _logger = new CompositeLogger();

        public virtual void SetReferences(IReferences references)
        {
            _logger.SetReferences(references);
        }

        private string CounterToString(Counter counter)
        {
            var result = "Counter " + counter.Name + " { ";
            result += "\"type\": " + counter.Type;
            if (counter.Last != null)
                result += ", \"last\": " + StringConverter.ToString(counter.Last);
            if (counter.Count != null)
                result += ", \"count\": " + StringConverter.ToString(counter.Count);
            if (counter.Min != null)
                result += ", \"min\": " + StringConverter.ToString(counter.Min);
            if (counter.Max != null)
                result += ", \"max\": " + StringConverter.ToString(counter.Max);
            if (counter.Average != null)
                result += ", \"avg\": " + StringConverter.ToString(counter.Average);
            if (counter.Time != null)
                result += ", \"time\": " + StringConverter.ToString(counter.Time);
            result += " }";
            return result;
        }

        /**
         * Outputs a list of counter values to log.
         * @param counter a list of counters to be dump to log.
         */
        protected override void Save(IEnumerable<Counter> counters)
        {
            if (_logger == null || counters == null)
                return;

            var countersArray = counters as Counter[] ?? counters.ToArray();

            if (!countersArray.Any()) return;

            new List<Counter>(countersArray)
                .Sort((c1, c2) => string.Compare(c1.Name, c2.Name, StringComparison.Ordinal));

            foreach (var counter in countersArray)
            {
                _logger.Info(null, CounterToString(counter));
            }
        }
    }
}
