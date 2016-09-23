using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Counters
{
    public class CompositeCounters : ICounters, ITimingCallback, IReferenceable, IDescribable
    {
        private List<ICounters> _counters = new List<ICounters>();
        private static Descriptor _descriptor = new Descriptor("pip-commons", "counters", "composite", "1.0");

        public CompositeCounters() { }

        public Descriptor GetDescriptor()
        {
            return _descriptor;
        }

        public void SetReferences(IReferences references)
        {
            var counters = references.GetOptional(new Descriptor(null, "counters", null, null));
            foreach (var counter in counters)
            {
                if (counter is ICounters)
                {
                    _counters.Add((ICounters)counter);
                }
            }
        }

        public Timing BeginTiming(string name)
        {
            return new Timing(name, this);
        }

        public void EndTiming(string name, float elapsed)
        {
            foreach (var counter in _counters)
            {
                if (counter is ITimingCallback)
                {
                    ((ITimingCallback)counter).EndTiming(name, elapsed);
                }
            }
        }

        public void Stats(string name, float value)
        {
            foreach (var counter in _counters)
            {
                counter.Stats(name, value);
            }
        }

        public void Last(string name, float value)
        {
            foreach (var counter in _counters)
            {
                counter.Last(name, value);
            }
        }

        public void TimestampNow(string name)
        {
            Timestamp(name, DateTime.Now);
        }

        public void Timestamp(string name, DateTime value)
        {
            foreach (var counter in _counters)
            {
                counter.Timestamp(name, value);
            }
        }

        public void IncrementOne(string name)
        {
            Increment(name, 1);
        }

        public void Increment(string name, int value)
        {
            foreach (var counter in _counters)
            {
                counter.Increment(name, value);
            }
        }
    }
}