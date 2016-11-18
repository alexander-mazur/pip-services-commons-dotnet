using System;

namespace PipServices.Commons.Refer
{
    public class Reference : ILocateable
    {
        public object Locator { get; }
        public object Refer { get; }

        private ILocateable _locateableReference;

        public Reference(object reference, object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

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

            _locateableReference = locatable;

            Refer = reference;

            if (describable != null)
                Locator = describable.GetDescriptor();
        }

        public bool Locate(object locator)
        {
            if (Refer.Equals(locator))
                return true;

            if (locator is Type)
			    return Equals(Refer.GetType(), locator);

		    // Locate locateable objects
		    if (_locateableReference != null)
                return _locateableReference.Locate(locator);

            // Locate by direct locator matching
            return Locator.Equals(locator);
        }
    }
}