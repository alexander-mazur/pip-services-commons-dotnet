using System;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Counters
{
    public class NullCounters : ICounters, IDescriptable
    {
        private static readonly Descriptor _descriptor = new Descriptor("pip-commons", "counters", "null", "1.0");

        public NullCounters()
        {
        }

        public Timing BeginTiming(string name)
        {
            return new Timing(name, null);
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

        public Descriptor GetDescriptor()
        {
            return _descriptor;
        }
    }
}