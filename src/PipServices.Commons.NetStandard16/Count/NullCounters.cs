using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class NullCounters : ICounters
    {
        public Timing BeginTiming(string name)
        {
            return new Timing();
        }

        public void Stats(string name, float value)
        {
        }

        public void Last(string name, float value)
        {
        }

        public void TimestampNow(string name)
        {
        }

        public void Timestamp(string name, DateTime value)
        {
        }

        public void IncrementOne(string name)
        {
        }

        public void Increment(string name, int value)
        {
        }
    }
}
