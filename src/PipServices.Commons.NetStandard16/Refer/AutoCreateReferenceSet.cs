using PipServices.Commons.Build;
using PipServices.Commons.Run;
using System;

namespace PipServices.Commons.Refer
{
    public class AutoCreateReferenceSet : ReferenceSet
    {
        private bool _autoOpen = true;

        public bool AutoOpen
        {
            get { return _autoOpen; }
            set { _autoOpen = value; }
        }

        protected IFactory FindFactory(object locator)
        {
            foreach (var reference in _references)
            {
                var factory = reference.GetComponent() as IFactory;
                if (factory != null)
                {
                    if (factory.CanCreate(locator))
                        return factory;
                }
            }

            return null;
        }

        protected virtual object Create(object locator)
        {
            // Find factory
            var factory = FindFactory(locator);

            if (factory == null)
                return null;

            try
            {
                // Create component
                var component = factory.Create(locator);

                if (component == null)
                    return null;

                // Replace locator
                if (component is IDescriptable)
                    locator = ((IDescriptable)component).GetDescriptor();

                return component;
            }
            catch (Exception ex)
            {
                throw new ReferenceException(null, locator)
                    .WithCause(ex);
            }
        }

        protected override object ResolveMissing(object locator)
        {
            var component = Create(locator);

            // Add to the list
            if (component != null)
                Put(component, locator);

            // Reference with other components
            var referenceable = component as IReferenceable;
            if (referenceable != null)
                referenceable.SetReferences(this);

            // Auto open component is configured
            var openable = component as IOpenable;
            if (openable != null && AutoOpen)
                openable.OpenAsync(null).Wait();

            return component;
        }
    }
}
