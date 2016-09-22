using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class ReferenceSet : IReferences
    {
        private List<ReferenceItem> _items = new List<ReferenceItem>();

        public ReferenceSet()
        { }

        public ReferenceSet(params ILocateable[] references)
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

        public object GetOneBefore(object reference, Locator locator)
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
                if (item.Reference.Equals(reference))
                    break;
            }

            for (; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locator.Match(locator))
                    return item.Reference;
            }

            throw new ReferenceNotFoundException(null, locator);
        }

        public object GetOneOptional(Locator locator)
        {
            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locator.Match(locator))
                    return item.Reference;
            }
            return null;
        }

        public object GetOneRequired(Locator locator)
        {
            var reference = GetOneOptional(locator);
            if (reference == null)
            {
                throw new ReferenceNotFoundException(null, locator);
            }
            return reference;
        }

        public List<object> GetOptional(Locator locator)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }

            var references = new List<object>();

            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Locator.Match(locator))
                    references.Add(item.Reference);
            }

            return references;
        }

        public List<object> GetRequired(Locator locator)
        {
            var references = GetOptional(locator);
            if (references.Count == 0)
            {
                throw new ReferenceNotFoundException(null, locator);
            }
            return references;
        }

        public void Remove(object reference)
        {
            if (reference == null) return;


            for (int index = _items.Count - 1; index >= 0; index--)
            {
                var item = _items[index];
                if (item.Reference.Equals(reference))
                {
                    // Remove from the set
                    _items.RemoveAt(index);
                    break;
                }
            }
        }

        public void Set(ILocateable reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            Set(reference.GetLocator(), reference);
        }

        public void Set(Locator locator, object reference)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            _items.Add(new ReferenceItem(locator, reference));

            if (reference is IReferenceable)
            {
                ((IReferenceable)reference).SetReferences(this);
            }
        }

        public static ReferenceSet From(params ILocateable[] references)
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