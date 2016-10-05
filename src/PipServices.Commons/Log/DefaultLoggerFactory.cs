using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public class DefaultLoggerFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-commons", "factory", "logger", "1.0");
        private static readonly Descriptor ConsoleLoggerDescriptor = new Descriptor("pip-commons", "logger", "console", "1.0");
        private static readonly Descriptor CompositeLoggerDescriptor = new Descriptor("pip-commons", "logger", "composite", "1.0");
        private static readonly Descriptor NullLoggerDescriptor = new Descriptor("pip-commons", "logger", "null", "1.0");

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
                return false;

            if (descriptor.Match(NullLoggerDescriptor))
                return new NullLogger();
            
            if (descriptor.Match(ConsoleLoggerDescriptor))
                return new ConsoleLogger();

            if (descriptor.Match(CompositeLoggerDescriptor))
                return new CompositeLogger();

            return null;
        }

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }
    }
}
