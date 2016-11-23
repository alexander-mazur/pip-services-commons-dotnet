using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Basic implementation of IReferences that stores component as a flat list
    /// </summary>
    public class ReferenceSet : IReferences
    {
        protected readonly List<Reference> References = new List<Reference>();
        private readonly object _lock = new object();

        public ReferenceSet() { }

        public ReferenceSet(IEnumerable<object> references)
        {
            foreach (var reference in references)
                Put(reference);
        }

        public void Put(object reference)
        {
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            lock (_lock)
            {
                var r = reference as Reference;
                References.Add(r ?? new Reference(reference));
            }
        }

        public void Put(object reference, object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));
            if (reference == null)
                throw new ArgumentNullException(nameof(reference));

            lock (_lock)
            {
                References.Add(new Reference(reference, locator));
            }
        }

        public object Remove(object locator)
        {
            if (locator == null) return null;

            lock (_lock)
            {
                for (int index = References.Count - 1; index >= 0; index--)
                {
                    var reference = References[index];
                    if (reference.Locate(locator))
                    {
                        // Remove from the set
                        References.RemoveAt(index);
                        return reference.GetReference();
                    }
                }
            }

            return null;
        }

        public List<object> GetAll()
        {
            return new List<object>(References);
        }

        /// <summary>
        /// Attempts to resolve missing reference
        /// </summary>
        /// <param name="locator">a locator to find references</param>
        /// <returns>resolved reference or <code>null<code></returns>
        protected virtual object ResolveMissing(object locator)
        {
            return null;
        }

        public object GetOneOptional(object locator)
        {
            lock (_lock)
            {
                for (var index = References.Count - 1; index >= 0; index--)
                {
                    var reference = References[index];
                    if (reference.Locate(locator))
                        return reference.GetReference();
                }
                return null;
            }
        }

        public object GetOneRequired(object locator)
        {
            var reference = GetOneOptional(locator) ?? ResolveMissing(locator);

            if (reference == null)
                throw new ReferenceException(null, locator);

            return reference;
        }

        public List<object> GetOptional(object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var references = new List<object>();

            lock (_lock)
            {
                for (int index = References.Count - 1; index >= 0; index--)
                {
                    var reference = References[index];
                    if (reference.Locate(locator))
                        references.Add(reference.GetReference());
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
                throw new ReferenceException(null, locator);

            return references;
        }

        public object GetOneBefore(object prior, object locator)
        {
            if (prior == null)
                throw new ArgumentNullException(nameof(prior));
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            lock (_lock)
            {
                var index = References.Count - 1;

                // Locate prior reference
                for (; index >= 0; index--)
                {
                    var reference = References[index];
                    if (reference.GetReference().Equals(prior))
                        break;
                }

                for (; index >= 0; index--)
                {
                    var reference = References[index];
                    if (reference.Locate(locator))
                        return reference.GetReference();
                }
            }

            throw new ReferenceException(null, locator);
        }

        public static ReferenceSet FromList(params IDescriptable[] references)
        {
            var result = new ReferenceSet();
            foreach (var reference in references)
                result.Put(reference);
            return result;
        }
    }
}