﻿using System;

namespace PipServices.Commons.Refer
{
    public class Reference : ILocateable
    {
        private object _locator;
        private object _reference;
        private ILocateable _locateableReference;

        public Reference(object reference, object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            _locator = locator;
            _reference = reference;
        }

        public Reference(object reference)
        {
            var locatable = reference as ILocateable;
            var describable = reference as IDescriptable;

            if (locatable == null && describable == null)
                throw new ArgumentException("Reference must implement ILocateable or IDescribable interface");

            _locateableReference = locatable;

            _reference = reference;

            if (describable != null)
                _locator = describable.GetDescriptor();
        }

        public bool Locate(object locator)
        {
            if (_reference.Equals(locator))
                return true;

            if (locator is Type)
                return ((Type)_locator).Equals(_reference.GetType());

		    // Locate locateable objects
		    if (_locateableReference != null)
                return _locateableReference.Locate(locator);

            // Locate by direct locator matching
            return _locator.Equals(locator);
        }

        public object GetReference()
        {
            return _reference;
        }
    }
}