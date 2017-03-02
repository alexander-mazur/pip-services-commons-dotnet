using System;
using System.Collections.Generic;

namespace PipServices.Commons.Build
{
    /// <summary>
    /// A factory that serves as a registry of factories.
    /// </summary>
    public class CompositeFactory : IFactory
    {
        private readonly List<IFactory> _factories = new List<IFactory>();

        public CompositeFactory() { }

        public CompositeFactory(params IFactory[] factories)
        {
            if (factories != null)
            {
                _factories.AddRange(factories);
            }
        }

        public void Add(IFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factories.Add(factory);
        }

        public void Remove(IFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factories.Remove(factory);
        }

        public object CanCreate(object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var factory = _factories.FindLast(x => x.CanCreate(locator) != null);

            return factory != null ? factory.CanCreate(locator) : null;
        }

        public object Create(object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var factory = _factories.FindLast(x => x.CanCreate(locator) != null);

            if (factory == null)
                throw new CreateException(null, locator);

            return factory.Create(locator);
        }
    }
}