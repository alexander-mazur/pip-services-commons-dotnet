using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public class DefaultLoggerFactory : IFactory
    {
        public static readonly Descriptor Descriptor = new Descriptor("pip-services-commons", "factory", "logger", "default", "1.0");
        public static readonly Descriptor NullLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "null", "*", "1.0");
        public static readonly Descriptor ConsoleLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "console", "*", "1.0");
        public static readonly Descriptor CompositeLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "composite", "*", "1.0");
        public static readonly Descriptor DiagnosticsLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "diagnostics", "*", "1.0");
        public static readonly Descriptor EventLoggerDescriptor = new Descriptor("pip-services-commons", "logger", "event", "*", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;
            if (descriptor == null)
                return false;

            if (descriptor.Match(NullLoggerDescriptor))
                return true;

            if (descriptor.Match(ConsoleLoggerDescriptor))
                return true;

            if (descriptor.Match(CompositeLoggerDescriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;
            if (descriptor == null)
                return null;

            if (descriptor.Match(NullLoggerDescriptor))
                return new NullLogger();

            if (descriptor.Match(ConsoleLoggerDescriptor))
                return new ConsoleLogger();

            if (descriptor.Match(CompositeLoggerDescriptor))
                return new CompositeLogger();

            return null;
        }
    }
}