using System;
using System.Collections.Generic;
using System.Linq;
using PipServices.Commons.Convert;
using PipServices.Commons.Log;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class LogCounters : CachedCounters, IDescriptable, IReferenceable
    {
        public static readonly Descriptor Locator = new Descriptor("pip-counters", "counters", "log", "1.0");

        private ILogger _logger;

        public Descriptor GetDescriptor()
        {
            return Locator;
        }

        public void SetReferences(IReferences references)
        {
            var loggerReference = references.GetOneOptional(new Descriptor(null, "logger", null, null));

            var logger = loggerReference as ILogger;
            if (logger != null)
			    _logger = logger;
        }

        /**
         * Formats counter string representation.
         * @param counter a counter object to generate a string for.
         * @return a formatted string representation of the counter.
         */
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

            var countersArr = counters as Counter[] ?? counters.ToArray();

            if (!countersArr.Any()) return;

            new List<Counter>(countersArr).Sort((c1, c2) => string.Compare(c1.Name, c2.Name, StringComparison.Ordinal));

            foreach (var counter in countersArr)
            {
                _logger.Debug(null, CounterToString(counter));
            }
        }
    }
}
