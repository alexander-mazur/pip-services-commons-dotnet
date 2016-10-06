using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Log
{
    public class DefaultLoggerFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-commons", "factory", "logger", "1.0");

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;
            if (descriptor == null)
                return false;
            
            if (descriptor.Match(NullLogger.Descriptor))
                return true;

            if (descriptor.Match(ConsoleLogger.Descriptor))
                return true;

            if (descriptor.Match(CompositeLogger.Descriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;
            if (descriptor == null)
                return false;

            if (descriptor.Match(NullLogger.Descriptor))
                return new NullLogger();
            
            if (descriptor.Match(ConsoleLogger.Descriptor))
                return new ConsoleLogger();

            if (descriptor.Match(CompositeLogger.Descriptor))
                return new CompositeLogger();

            return null;
        }

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }
    }
}
