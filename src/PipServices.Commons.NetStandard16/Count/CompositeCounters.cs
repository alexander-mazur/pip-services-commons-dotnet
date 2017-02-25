using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public class CompositeCounters : ICounters, ITimingCallback, IReferenceable
    {
        protected readonly List<ICounters> _counters = new List<ICounters>();

        public CompositeCounters(IReferences references = null)
        {
            if (references != null) SetReferences(references);
        }

        public virtual void SetReferences(IReferences references)
        {
            var counters = references.GetOptional<ICounters>(new Descriptor(null, "counters", null, null, null));
            foreach (var counter in counters)
            {
                if (counter != this)
                    _counters.AddRange(counters);
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
                if (callback != null)
                    callback.EndTiming(name, elapsed);
            }
        }

        public void Stats(string name, float value)
        {
            foreach (var counter in _counters)
                counter.Stats(name, value);
        }

        public void Last(string name, float value)
        {
            foreach (var counter in _counters)
                counter.Last(name, value);
        }

        public void TimestampNow(string name)
        {
            Timestamp(name, DateTime.UtcNow);
        }

        public void Timestamp(string name, DateTime value)
        {
            foreach (var counter in _counters)
                counter.Timestamp(name, value);
        }

        public void IncrementOne(string name)
        {
            Increment(name, 1);
        }

        public void Increment(string name, int value)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            foreach (var counter in _counters)
                counter.Increment(name, value);
        }
    }
}
