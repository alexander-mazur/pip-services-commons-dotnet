using System;
using System.Collections.Generic;
using PipServices.Commons.Config;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Count
{
    public abstract class CachedCounters : ICounters, IReconfigurable, ITimingCallback
    {
        private static readonly long _defaultInterval = 300000;

        private readonly IDictionary<string, Counter> _cache = new Dictionary<string, Counter>();
        private bool _updated;
        private long _lastDumpTime = Environment.TickCount;
        private long _interval = _defaultInterval;
        private readonly object _lock = new object();

        public CachedCounters() { }

        public long Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        protected abstract void Save(IEnumerable<Counter> counters);

        public virtual void Configure(ConfigParams config)
        {
            _interval = config.GetAsLongWithDefault("interval", _defaultInterval);
        }

        public void Clear(string name)
        {
            lock(_lock)
            {
                _cache.Remove(name);
            }
        }

        public void ClearAll()
        {
            lock(_lock)
            {
                _cache.Clear();
                _updated = false;
            }
        }

        public void Dump()
        {
            if (!_updated) return;

            var counters = GetAll();

            Save(counters);

            lock(_lock)
            {
                _updated = false;
                _lastDumpTime = Environment.TickCount;
            }
        }

        protected void Update()
        {
            _updated = true;
            if (Environment.TickCount > _lastDumpTime + _interval)
            {
                try
                {
                    Dump();
                }
                catch (InvocationException)
                {
                    // Todo: decide what to do
                }
            }
        }

        public IEnumerable<Counter> GetAll()
        {
            lock(_lock)
            {
                return _cache.Values;
            }
        }

        public Counter Get(string name, CounterType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            lock (_lock) {
                Counter counter;

                _cache.TryGetValue(name, out counter);

                if (counter == null || counter.Type != type)
                {
                    counter = new Counter(name, type);
                    _cache[name] = counter;
                }

                return counter;
            }
        }

        private void CalculateStats(Counter counter, double value)
        {
            if (counter == null)
                throw new ArgumentNullException(nameof(counter));

            counter.Last = value;
            counter.Count = counter.Count + 1 ?? 1;
            counter.Max= counter.Max.HasValue ? Math.Max(counter.Max.Value, value) : value;
            counter.Min = counter.Min.HasValue ? Math.Min(counter.Min.Value, value) : value;
            counter.Average = (counter.Average.HasValue && counter.Count > 1 
                ? (counter.Average*(counter.Count - 1) + value) / counter.Count : value);
        }

        public Timing BeginTiming(string name)
        {
            return new Timing(name, this);
        }

        public void EndTiming(string name, double elapsed)
        {
            var counter = Get(name, CounterType.Interval);
            CalculateStats(counter, elapsed);
            Update();
        }

        public void Stats(string name, float value)
        {
            var counter = Get(name, CounterType.Statistics);
            CalculateStats(counter, value);
            Update();
        }

        public void Last(string name, float value)
        {
            var counter = Get(name, CounterType.LastValue);
            counter.Last = value;
            Update();
        }

        public void TimestampNow(string name)
        {
            Timestamp(name, DateTime.UtcNow);
        }

        public void Timestamp(string name, DateTime value)
        {
            var counter = Get(name, CounterType.Timestamp);
            counter.Time = value;
            Update();
        }

        public void IncrementOne(string name)
        {
            Increment(name, 1);
        }
        
        public void Increment(string name, int value)
        {
            var counter = Get(name, CounterType.Increment);
            counter.Count = counter.Count.HasValue ? counter.Count + value : value;
            Update();
        }
    }
}
