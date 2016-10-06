using System;
using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class DefaultCountersFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-commons", "factory", "counters", "1.0");
        private static readonly Descriptor LogCounterDescriptor = new Descriptor("pip-commons", "counters", "log", "1.0");
        private static readonly Descriptor CompositeCounterDescriptor = new Descriptor("pip-commons", "counters", "composite", "1.0");
        private static readonly Descriptor NullCounterDescriptor = new Descriptor("pip-commons", "counters", "null", "1.0");

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCounterDescriptor))
                return true;

            if (descriptor.Match(LogCounterDescriptor))
                return true;

            if (descriptor.Match(CompositeCounterDescriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCounterDescriptor))
                return new NullCounters();

            if (descriptor.Match(LogCounterDescriptor))
                return new LogCounters();

            if (descriptor.Match(CompositeCounterDescriptor))
                return new CompositeCounters();

            return null;
        }
    }
}
