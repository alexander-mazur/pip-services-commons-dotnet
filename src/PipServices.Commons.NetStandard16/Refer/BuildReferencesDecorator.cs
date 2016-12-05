using PipServices.Commons.Build;
using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public class BuildReferencesDecorator: ReferencesDecorator
    {
        public BuildReferencesDecorator(IReferences baseReferences = null, IReferences parentReferences = null)
            : base(baseReferences, parentReferences)
        {
            BuildEnabled = true;
        }

        public bool BuildEnabled { get; set; }

        private IFactory FindFactory(object locator)
        {
            foreach (var component in GetAll())
            {
                var factory = component as IFactory;
                if (factory != null)
                {
                    if (factory.CanCreate(locator))
                        return factory;
                }
            }

            return null;
        }

        private object CreateComponent(object locator)
        {
            // Find factory
            var factory = FindFactory(locator);
            if (factory == null) return null;

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
            catch (Exception)
            {
                return null;
            }
        }

        public override List<T> Find<T>(ReferenceQuery query, bool required)
        {
            var components = base.Find<T>(query, false);

            // Try to create component
            if (components.Count == 0 && BuildEnabled)
            {
                var component = CreateComponent(query.Locator);
                if (component is T)
                {
                    ParentReferences.Put(component);
                    components.Add((T)component);
                }
            }

            // Throw exception is no required components found
            if (required && components.Count == 0)
                throw new ReferenceException(query.Locator);

            return components;
        }
    }
}
