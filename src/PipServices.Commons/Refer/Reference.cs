using System;

namespace PipServices.Commons.Refer
{
    public class Reference : ILocateable
    {
        public Descriptor Descriptor { get; }
        public object Ref { get; }

        public Reference(Descriptor descriptor, object reference)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }
            Descriptor = descriptor;
            Ref = reference;
        }

        public Reference(object reference)
        {
            var locatable = reference as ILocateable;
            var describable = reference as IDescribable;

            if (locatable == null && describable == null)
            {
                throw new ArgumentException("Reference must implement ILocateable or IDescribable interface");
            }

            Ref = reference;
            if (describable != null)
            {
                Descriptor = describable.GetDescriptor();
            }
        }

        public bool Locate(object descriptor)
        {
            if (Ref.Equals(descriptor))
            {
                return true;
            }

            var locatable = Ref as ILocateable;
            if (locatable != null && locatable.Locate(descriptor))
            {
                return true;
            }
            return Descriptor.Equals(descriptor);

        }
    }
}