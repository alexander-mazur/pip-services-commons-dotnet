using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public class DefaultCountersFactory : Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "counters", "default", "1.0");
        public static readonly Descriptor NullCountersDescriptor = new Descriptor("pip-services-commons", "counters", "null", "*", "1.0");
        public static readonly Descriptor LogCountersDescriptor = new Descriptor("pip-services-commons", "counters", "log", "*", "1.0");
        public static readonly Descriptor CompositeCountersDescriptor = new Descriptor("pip-services-commons", "counters", "composite", "*", "1.0");

        public DefaultCountersFactory()
        {
            RegisterAsType(NullCountersDescriptor, typeof(NullCounters));
            RegisterAsType(LogCountersDescriptor, typeof(LogCounters));
            RegisterAsType(CompositeCountersDescriptor, typeof(CompositeCounters));
	    }
    }
}
