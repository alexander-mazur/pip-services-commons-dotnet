using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public class ReferenceSet : IReferences
    {
        protected readonly List<Reference> References = new List<Reference>();
        private readonly object _lock = new object();

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
            return new List<object>(References);
        }

        /**
         * Attempts to resolve missing reference
         * @param locator a locator to find references
         * @return resolved reference or <code>null<code>
         */
        protected virtual object ResolveMissing(object locator)
        {
            return null;
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

            var index = References.Count - 1;

            lock (_lock)
            {
                // Locate prior reference
                for (; index >= 0; index--)
                {
                    var item = References[index];
                    if (item.Refer.Equals(reference))
                        break;
                }

                for (; index >= 0; index--)
                {
                    var item = References[index];
                    if (item.Locate(locator))
                        return item.Refer;
                }
            }

            throw new ReferenceNotFoundException(null, locator);
        }

        public object GetOneOptional(object locator)
        {
            lock (_lock)
            {
                for (var index = References.Count - 1; index >= 0; index--)
                {
                    var item = References[index];
                    if (item.Locate(locator))
                        return item.Refer;
                }
                return null;
            }
        }

        public object GetOneRequired(object locator)
        {
            var reference = GetOneOptional(locator) ?? ResolveMissing(locator);

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

            lock (_lock)
            {
                for (int index = References.Count - 1; index >= 0; index--)
                {
                    var item = References[index];
                    if (item.Locate(locator))
                        references.Add(item.Refer);
                }
            }

            return references;
        }

        public List<object> GetRequired(object locator)
        {
            var references = GetOptional(locator);

            // Try to resolve missing dependency
            if (references.Count == 0)
            {
                var reference = ResolveMissing(locator);

                lock (_lock)
                {
                    if (reference != null)
                        references.Add(reference);
                }
            }

            if (references.Count == 0)
            {
                throw new ReferenceException(null, locator);
            }
            return references;
        }

        public object Remove(object locator)
        {
            if (locator == null) return null;

            lock (_lock)
            {
                for (int index = References.Count - 1; index >= 0; index--)
                {
                    var item = References[index];
                    if (item.Locate(locator))
                    {
                        // Remove from the set
                        References.RemoveAt(index);
                        return item.Refer;
                    }
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

            lock (_lock)
            {
                References.Add(r ?? new Reference(reference));
            }
        }

        public void Put(object reference, object locator)
        {
            if (locator == null)
            {
                throw new ArgumentNullException(nameof(locator));
            }
            if (reference == null)
            {
                throw new ArgumentNullException(nameof(reference));
            }

            lock (_lock)
            {
                References.Add(new Reference(reference, locator));
            }
        }

        public static ReferenceSet FromList(params IDescriptable[] references)
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