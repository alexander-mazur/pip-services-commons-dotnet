using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public class ReferenceSet : IReferences
    {
        private List<Reference> _items = new List<Reference>();

        public ReferenceSet()
        { }

        public ReferenceSet(params IDescribable[] references)
        {
            foreach (var reference in references)
            {
                Set(reference);
            }
        }

        public List<object> GetAll()
        {
            return new List<object>(_items);
        }

        public object GetOneBefore(object reference, Descriptor descriptor)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            int index = _items.Count - 1;

            // Locate prior reference
            for (; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Ref.Equals(reference))
                    break;
            }

            for (; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Descriptor.Match(descriptor))
                    return item.Ref;
            }

            throw new ReferenceNotFoundException(null, descriptor);
        }

        public object GetOneOptional(Descriptor descriptor)
        {
            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Descriptor.Match(descriptor))
                    return item.Ref;
            }
            return null;
        }

        public object GetOneRequired(Descriptor descriptor)
        {
            var reference = GetOneOptional(descriptor);
            if (reference == null)
            {
                throw new ReferenceNotFoundException(null, descriptor);
            }
            return reference;
        }

        public List<object> GetOptional(Descriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            var references = new List<object>();

            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Descriptor.Match(descriptor))
                    references.Add(item.Ref);
            }

            return references;
        }

        public List<object> GetRequired(Descriptor descriptor)
        {
            var references = GetOptional(descriptor);
            if (references.Count == 0)
            {
                throw new ReferenceNotFoundException(null, descriptor);
            }
            return references;
        }

        public void Remove(object reference)
        {
            if (reference == null) return;


            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Ref.Equals(reference))
                {
                    // Remove from the set
                    _items.RemoveAt(index);
                    break;
                }
            }
        }

        public void Set(IDescribable reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            Set(reference.GetDescriptor(), reference);
        }

        public void Set(Descriptor descriptor, object reference)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _items.Add(new Reference(descriptor, reference));

            if (reference is IReferenceable)
            {
                ((IReferenceable)reference).SetReferences(this);
            }
        }

        public static ReferenceSet From(params IDescribable[] references)
        {
            var result = new ReferenceSet();
            foreach (var reference in references)
            {
                result.Set(reference);
            }
            return result;
        }
    }
}