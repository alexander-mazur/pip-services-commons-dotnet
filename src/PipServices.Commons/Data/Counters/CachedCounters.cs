using System;
using System.Collections.Generic;

namespace PipServices.Commons.Counters
{
    public abstract class CachedCounters : ICounters, ITimingCallback
    {
        private Dictionary<string, Counter> _cache = new Dictionary<string, Counter>();
        private bool _updated = false;

        protected CachedCounters() { }

        protected abstract void Save(List<Counter> counters);

        public void Reset(string name)
        {
            _cache.Remove(name);
        }

        public void ResetAll()
        {
            _cache.Clear();
            _updated = false;
        }

        public void Dump()
        {
            if (_updated)
            {
                Save(GetAll());
            }
        }

        public List<Counter> GetAll()
        {
            return new List<Counter>(_cache.Values);
        }

        public Counter Get(string name, int type)
        {
            if (name == null || name.Length == 0)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var counter = _cache[name];

            if (counter == null || counter.Type != (CounterType)type)
            {
                counter = new Counter(name, (CounterType)type);
                _cache[name] = counter;
            }

            return counter;
        }

        private void CalculateStats(Counter counter, float value)
        {
            if (counter == null)
            {
                throw new ArgumentNullException(nameof(counter));
            }

            counter.Last = value;
            counter.Count = counter.Count != null ? counter.Count + 1 : 1;
            counter.Max = counter.Max != null ? Math.Max(counter.Max.Value, value) : value;
            counter.Min = counter.Min != null ? Math.Min(counter.Min.Value, value) : value;
            counter.Avg = counter.Avg != null && counter.Count > 1
                ? ((counter.Avg * (counter.Count - 1)) + value) / counter.Count : value;

            _updated = true;
        }

        public Timing BeginTiming(string name)
        {
            return new Timing(name, this);
        }

        public void EndTiming(string name, float elapsed)
        {
            var counter = Get(name, (int)CounterType.Interval);
            CalculateStats(counter, elapsed);
        }

        public void Stats(string name, float value)
        {
            var counter = Get(name, (int)CounterType.Statistics);
            CalculateStats(counter, value);
        }

        public void Last(string name, float value)
        {
            var counter = Get(name, (int)CounterType.LastValue);
            counter.Last = value;
            _updated = true;
        }

        public void TimestampNow(string name)
        {
            Timestamp(name, DateTime.Now);
        }

        public void Timestamp(string name, DateTime value)
        {
            var counter = Get(name, (int)CounterType.Timestamp);
            counter.Time = value != null ? value : DateTime.Now;
            _updated = true;
        }

        public void IncrementOne(string name)
        {
            Increment(name, 1);
        }

        public void Increment(string name, int value)
        {
            var counter = Get(name, (int)CounterType.Increment);
            counter.Count = counter.Count != null ? counter.Count + value : value;
            _updated = true;
        }
    }
}