using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class DefaultCountersFactory : IFactory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "counters", "default", "1.0");
        public static readonly Descriptor NullCountersDescriptor = new Descriptor("pip-services-commons", "counters", "null", "*", "1.0");
        public static readonly Descriptor LogCountersDescriptor = new Descriptor("pip-services-commons", "counters", "log", "*", "1.0");
        public static readonly Descriptor CompositeCountersDescriptor = new Descriptor("pip-services-commons", "counters", "composite", "*", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCountersDescriptor))
                return true;

            if (descriptor.Match(LogCountersDescriptor))
                return true;

            if (descriptor.Match(CompositeCountersDescriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(NullCountersDescriptor))
                return new NullCounters();

            if (descriptor.Match(LogCountersDescriptor))
                return new LogCounters();

            if (descriptor.Match(CompositeCountersDescriptor))
                return new CompositeCounters();

            return null;
        }
    }
}
