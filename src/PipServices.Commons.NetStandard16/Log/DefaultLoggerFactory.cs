using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public class DefaultLoggerFactory : Factory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "logger", "default", "1.0");
        public static readonly Descriptor NullLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "null", "*", "1.0");
        public static readonly Descriptor ConsoleLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "console", "*", "1.0");
        public static readonly Descriptor CompositeLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "composite", "*", "1.0");
        public static readonly Descriptor DiagnosticsLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "diagnostics", "*", "1.0");
        public static readonly Descriptor EventLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "event", "*", "1.0");

        public DefaultLoggerFactory()
        {
            RegisterAsType(NullLoggerDescriptor, typeof(NullLogger));
            RegisterAsType(ConsoleLoggerDescriptor, typeof(ConsoleLogger));
            RegisterAsType(CompositeLoggerDescriptor, typeof(CompositeLogger));
	    }

    }
}