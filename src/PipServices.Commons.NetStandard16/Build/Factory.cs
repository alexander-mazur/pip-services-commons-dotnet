using PipServices.Commons.Reflect;
using System;
using System.Collections.Generic;

namespace PipServices.Commons.Build
{
    public class Factory: IFactory
    {
        private class Registration
        {
            public Registration(Object locator, Func<object, object> factory)
            {
                Locator = locator;
                Factory = factory;
            }

            public object Locator { get; private set; }
            public Func<object, object> Factory { get; private set; }
        }

        private List<Registration> _registrations = new List<Registration>();

        public void Register(object locator, Func<object, object> factory)
        {
            if (locator == null)
                throw new NullReferenceException("Locator cannot be null");
            if (factory == null)
                throw new NullReferenceException("Factory cannot be null");

            _registrations.Add(new Registration(locator, factory));
        }

        public void RegisterAsType(object locator, Type type)
        {
            if (locator == null)
                throw new NullReferenceException("Locator cannot be null");
            if (type == null)
                throw new NullReferenceException("Type cannot be null");

            Func<object, object> factory = (_) =>
            {
                try
                {
                    return TypeReflector.CreateInstanceByType(type);
                }
                catch
                {
                    return null;
                }
            };
            _registrations.Add(new Registration(locator, factory));
        }

        public object CanCreate(object locator)
        {
            foreach (Registration registration in _registrations)
            {
                object thisLocator = registration.Locator;
                if (thisLocator.Equals(locator))
                    return thisLocator;
            }
            return null;
        }

        public object Create(object locator)
        {
		    foreach (Registration registration in _registrations) {
                if (registration.Locator.Equals(locator))
                {
                    try
                    {
                        return registration.Factory(locator);
                    }
                    catch (Exception ex)
                    {
                        if (ex is CreateException)
						    throw;

                        throw (CreateException)new CreateException(
                            null,
                            "Failed to create object for " + locator
                        ).WithCause(ex);
                    }
                }
            }
		    return null;
        }
	
    }
}
