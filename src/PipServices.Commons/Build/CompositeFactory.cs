using System;
using System.Collections.Generic;
using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    /// <summary>
    /// A factory that serves as a registry of factories.
    /// </summary>
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

            return _factories.Exists(x => x.CanCreate(descriptor));
        }

        public object Create(Descriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }


            var factory = _factories.FindLast(x => x.CanCreate(descriptor));
            if (factory == null)
            {
                throw new CreateException(null, descriptor);
            }
            return factory.Create(descriptor);
        }
    }
}