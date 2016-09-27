using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public class ReferenceSet : IReferences
    {
        private List<Reference> _items = new List<Reference>();

        public ReferenceSet()
        { }

        public ReferenceSet(params IDescriptable[] references)
        {
            foreach (var reference in references)
            {
                Put(reference);
            }
        }

        public List<object> GetAll()
        {
            return new List<object>(_items);
        }

        public object GetOneBefore(object reference, object locator)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            int index = _items.Count - 1;

            // Locate prior reference
            for (; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Refer.Equals(reference))
                    break;
            }

            for (; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locate(locator))
                    return item.Refer;
            }

            throw new ReferenceNotFoundException(null, locator);
        }

        public object GetOneOptional(object locator)
        {
            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locate(locator))
                    return item.Refer;
            }
            return null;
        }

        public object GetOneRequired(object locator)
        {
            var reference = GetOneOptional(locator);
            if (reference == null)
            {
                throw new ReferenceNotFoundException(null, locator);
            }
            return reference;
        }

        public List<object> GetOptional(object locator)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            var references = new List<object>();

            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locate(locator))
                    references.Add(item.Refer);
            }

            return references;
        }

        public List<object> GetRequired(object locator)
        {
            var references = GetOptional(locator);
            if (references.Count == 0)
            {
                throw new ReferenceNotFoundException(null, locator);
            }
            return references;
        }

        public object Remove(object locator)
        {
            if (locator == null) return null;


            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locate(locator))
                {
                    // Remove from the set
                    _items.RemoveAt(index);
                    return item.Refer;
                }
            }
            return null;
        }

        public void Put(object reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            var r = reference as Reference;
            if(r != null)
            {
                _items.Add(r);
            }
            else
            {
                _items.Add(new Reference(reference));
            }
        }

        public void Put(object locator, object reference)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _items.Add(new Reference(locator, reference));
        }

        public static ReferenceSet From(params IDescriptable[] references)
        {
            var result = new ReferenceSet();
            foreach (var reference in references)
            {
                result.Put(reference);
            }
            return result;
        }
    }
}