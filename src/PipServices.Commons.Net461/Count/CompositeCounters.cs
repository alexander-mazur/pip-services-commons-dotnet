using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class CompositeCounters : ICounters, ITimingCallback, IReferenceable, IDescriptable
    {
        public static Descriptor Descriptor { get; } = new Descriptor("pip-commons", "counters", "composite", "1.0");

        private readonly IList<ICounters> _counters = new List<ICounters>();

        public Descriptor GetDescriptor() { return Descriptor; }

        public void SetReferences(IReferences references)
        {
            var counters = references.GetOptional(new Descriptor(null, "counters", null, null));

            foreach (var counter in counters)
            {
                var item = counter as ICounters;
                if (item != null)
				    _counters.Add(item);
            }
        }

        public Timing BeginTiming(string name)
        {
            return new Timing(name, this);
        }

        public void EndTiming(string name, double elapsed)
        {
            foreach (var counter in _counters)
            {
                var callback = counter as ITimingCallback;

                callback?.EndTiming(name, elapsed);
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
            Timestamp(name, DateTime.UtcNow);
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
            if (name == null) throw new ArgumentNullException(nameof(name));
            foreach (var counter in _counters)
            {
                counter.Increment(name, value);
            }
        }
    }
}
