using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Count
{
    public sealed class DefaultCountersFactory : IFactory, IDescriptable
    {
        private static readonly Descriptor ThisDescriptor = new Descriptor("pip-commons", "factory", "counters", "1.0");

        public Descriptor GetDescriptor()
        {
            return ThisDescriptor;
        }

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(NullCounters.Descriptor))
                return true;

            if (descriptor.Match(LogCounters.Descriptor))
                return true;

            if (descriptor.Match(CompositeCounters.Descriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(NullCounters.Descriptor))
                return new NullCounters();

            if (descriptor.Match(LogCounters.Descriptor))
                return new LogCounters();

            if (descriptor.Match(CompositeCounters.Descriptor))
                return new CompositeCounters();

            return null;
        }
    }
}
