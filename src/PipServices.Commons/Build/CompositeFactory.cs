using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    public class CompositeFactory
    {
        private List<IFactory> _factories = new List<IFactory>();

        public CompositeFactory()
        { }

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
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _factories.Add(factory);
        }

        public void Remove(IFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _factories.Remove(factory);
        }

        public bool CanCreate(Locator locator)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            // Iterate from the latest factories
            for (int index = _factories.Count - 1; index >= 0; index--)
            {
                if (_factories[index].CanCreate(locator))
                    return true;
            }

            return false;
        }
    }
}