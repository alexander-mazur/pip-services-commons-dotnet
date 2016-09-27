using System;

namespace PipServices.Commons.Refer
{
    public class Reference : ILocateable
    {
        public object Locator { get; }
        public object Refer { get; }

        public Reference(object locator, object reference)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }
            Locator = locator;
            Refer = reference;
        }

        public Reference(object reference)
        {
            var locatable = reference as ILocateable;
            var describable = reference as IDescriptable;

            if (locatable == null && describable == null)
            {
                throw new ArgumentException("Reference must implement ILocateable or IDescribable interface");
            }

            Refer = reference;
            if (describable != null)
            {
                Locator = describable.GetDescriptor();
            }
        }

        public bool Locate(object descriptor)
        {
            if (Refer.Equals(descriptor))
            {
                return true;
            }

            var locatable = Refer as ILocateable;
            if (locatable != null && locatable.Locate(descriptor))
            {
                return true;
            }
            return Locator.Equals(descriptor);

        }
    }
}