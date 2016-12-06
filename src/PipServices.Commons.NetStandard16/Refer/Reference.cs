using System;

namespace PipServices.Commons.Refer
{
    public class Reference : ILocateable
    {
        private object _locator;
        private object _component;
        private ILocateable _locateable;

        public Reference(object component, object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            if (component == null)
                throw new ArgumentNullException(nameof(component));

            _locator = locator;
            _component = component;
        }

        public Reference(object reference)
        {
            var locatable = reference as ILocateable;
            var descriptable = reference as IDescriptable;

            if (locatable == null && descriptable == null)
                throw new ArgumentException("Reference must implement ILocateable or IDescriptable interface");

            _locateable = locatable;

            _component = reference;

            if (descriptable != null)
                _locator = descriptable.GetDescriptor();
        }

        public bool Locate(object locator)
        {
            if (_component.Equals(locator))
                return true;

            if (locator is Type)
                return ((Type)_locator).Equals(_component.GetType());

		    // Locate locateable objects
		    if (_locateable != null)
                return _locateable.Locate(locator);

            // Locate by direct locator matching
            return _locator.Equals(locator);
        }

        public object GetComponent()
        {
            return _component;
        }
    }
}