using System;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Basic implementation of IReferences that stores component as a flat list
    /// </summary>
    public class ReferenceSet : IReferences
    {
        protected readonly List<Reference> _references = new List<Reference>();
        protected readonly object _lock = new object();

        public ReferenceSet() { }

        public ReferenceSet(IEnumerable<object> references)
        {
            foreach (var reference in references)
                Put(reference);
        }

        public virtual void Put(object component, object locator = null)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            lock (_lock)
            {
                if (locator != null)
                    _references.Add(new Reference(component, locator));
                else if (component is Reference)
                    _references.Add((Reference)component);
                else
                    _references.Add(new Reference(component));
            }
        }

        public void PutAll(params object[] components)
        {
            foreach (var component in components)
                Put(component);
        }

        public virtual object Remove(object locator)
        {
            if (locator == null) return null;

            lock (_lock)
            {
                for (int index = _references.Count - 1; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Locate(locator))
                    {
                        // Remove from the set
                        _references.RemoveAt(index);
                        return reference.GetComponent();
                    }
                }
            }

            return null;
        }

        public virtual List<object> GetAll()
        {
            var components = new List<object>();
            foreach (var reference in _references)
                components.Add(reference.GetComponent());
            return components;
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

        protected virtual T ResolveMissing<T>(object locator)
        {
            var component = ResolveMissing(locator);
            if (component is T)
                return (T)component;
            else
                return default(T);
        }

        public virtual object GetOneOptional(object locator)
        {
            return GetOneOptional<object>(locator);
        }

        public virtual T GetOneOptional<T>(object locator)
        {
            lock (_lock)
            {
                for (var index = _references.Count - 1; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Locate(locator))
                    {
                        var component = reference.GetComponent();
                        if (component is T)
                            return (T)component;
                    }
                }
                return default(T);
            }
        }

        public virtual object GetOneRequired(object locator)
        {
            return GetOneRequired<object>(locator);
        }

        public virtual T GetOneRequired<T>(object locator)
        {
            var component = GetOneOptional<T>(locator);

            // Create a missing component
            if (component == null)
                component = ResolveMissing<T>(locator);

            if (component == null)
                throw new ReferenceException(null, locator);

            return component;
        }

        public virtual List<object> GetOptional(object locator)
        {
            return GetOptional<object>(locator);
        }

        public virtual List<T> GetOptional<T>(object locator)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            var components = new List<T>();

            lock (_lock)
            {
                for (int index = _references.Count - 1; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Locate(locator))
                    {
                        var component = reference.GetComponent();
                        if (component is T)
                            components.Add((T)component);
                    }
                }
            }

            return components;
        }

        public List<object> GetRequired(object locator)
        {
            return GetRequired<object>(locator);
        }

        public List<T> GetRequired<T>(object locator)
        {
            var components = GetOptional<T>(locator);

            // Try to resolve missing dependency
            if (components.Count == 0)
            {
                var component = ResolveMissing<T>(locator);

                lock (_lock)
                {
                    if (component != null)
                        components.Add(component);
                }
            }

            if (components.Count == 0)
                throw new ReferenceException(null, locator);

            return components;
        }

        public object GetOneBefore(object prior, object locator)
        {
            return GetOneBefore<object>(prior, locator);
        }

        public T GetOneBefore<T>(object prior, object locator)
        {
            if (prior == null)
                throw new ArgumentNullException(nameof(prior));
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));

            lock (_lock)
            {
                var index = _references.Count - 1;

                // Locate prior reference
                for (; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.GetComponent().Equals(prior))
                        break;
                }

                for (; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Locate(locator))
                    {
                        var component = reference.GetComponent();
                        if (component is T)
                            return (T)component;
                    }
                }
            }

            throw new ReferenceException(null, locator);
        }

        public static ReferenceSet FromList(params object[] components)
        {
            var result = new ReferenceSet();
            result.PutAll(components);
            return result;
        }
    }
}