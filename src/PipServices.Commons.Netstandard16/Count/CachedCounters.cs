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

        protected abstract void Save(IEnumerable<Counter> counters);

        public void Configure(ConfigParams config)
        {
            _interval = config.GetAsLongWithDefault("interval", _defaultInterval);
        }

        public void Clear(string name)
        {
            lock(_lock) {
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
            if (!_updated)
                return;

            var counters = GetAll();

            Save(counters);

            lock(_lock) {
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
            counter.Average = (counter.Average.HasValue && counter.Count > 1 ? (counter.Average*(counter.Count - 1) + value)/counter.Count : value);
        }

        /**
         * Starts measurement of execution time interval.
         * The method returns ITiming object that provides endTiming()
         * method that shall be called when execution is completed
         * to calculate elapsed time and update the counter.
         * @param name the name of interval counter.
         * @return callback interface with endTiming() method 
         * that shall be called at the end of execution.
         */
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

        /**
         * Calculates rolling statistics: minimum, maximum, average
         * and updates Statistics counter.
         * This counter can be used to measure various non-functional
         * characteristics, such as amount stored or transmitted data,
         * customer feedback, etc. 
         * @param name the name of statistics counter.
         * @param value the value to add to statistics calculations.
         */
        public void Stats(string name, float value)
        {
            var counter = Get(name, CounterType.Statistics);
            CalculateStats(counter, value);
            Update();
        }

        /**
         * Records the last reported value. 
         * This counter can be used to store performance values reported
         * by clients or current numeric characteristics such as number
         * of values stored in cache.
         * @param name the name of last value counter
         * @param value the value to be stored as the last one
         */
        public void Last(string name, float value)
        {
            var counter = Get(name, CounterType.LastValue);
            counter.Last = value;
            Update();
        }

        /**
         * Records the current time.
         * This counter can be used to track timing of key
         * business transactions.
         * @param name the name of timing counter
         */
        public void TimestampNow(string name)
        {
            Timestamp(name, DateTime.UtcNow);
        }

        /**
         * Records specified time.
         * This counter can be used to tack timing of key
         * business transactions as reported by clients.
         * @param name the name of timing counter.
         * @param value the reported timing to be recorded.
         */
        public void Timestamp(string name, DateTime value)
        {
            var counter = Get(name, CounterType.Timestamp);
            counter.Time = value;
            Update();
        }

        /**
         * Increments counter by value of 1.
         * This counter is often used to calculate
         * number of client calls or performed transactions.
         * @param name the name of counter counter.
         */
        public void IncrementOne(string name)
        {
            Increment(name, 1);
        }
        
        /**
         * Increments counter by specified value.
         * This counter can be used to track various
         * numeric characteristics
         * @param name the name of the increment value.
         * @param value number to increase the counter.
         */
        public void Increment(string name, int value)
        {
            var counter = Get(name, CounterType.Increment);
            counter.Count = counter.Count.HasValue ? counter.Count + value : value;
            Update();
        }
    }
}
