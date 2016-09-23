using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    public class CompositeFactory : IFactory
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

        public bool CanCreate(Descriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            // Iterate from the latest factories
            for (int index = _factories.Count - 1; index >= 0; index--)
            {
                if (_factories[index].CanCreate(descriptor))
                    return true;
            }

            return false;
        }

        public object Create(Descriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            // Iterate from the latest factories
            for (int index = _factories.Count - 1; index >= 0; index--)
            {
                if (_factories[index].CanCreate(descriptor))
                    return _factories[index].Create(descriptor);
            }

            return false;
        }
    }
}