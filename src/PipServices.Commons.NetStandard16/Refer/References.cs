using System;
using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Basic implementation of IReferences that stores component as a flat list
    /// </summary>
    public class References : IReferences
    {
        protected readonly List<Reference> _references = new List<Reference>();
        protected readonly object _lock = new object();

        public References() { }

        public References(object[] tuples)
        {
            if (tuples != null)
            {
                for (int index = 0; index < tuples.Length; index += 2)
                {
                    if (index + 1 >= tuples.Length) break;

                    Put(tuples[index], tuples[index + 1]);
                }
            }
        }

        public virtual void Put(object locator, object component)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            lock (_lock)
            {
                _references.Add(new Reference(locator, component));
            }
        }

        public virtual object Remove(object locator)
        {
            if (locator == null) return null;

            lock (_lock)
            {
                for (int index = _references.Count - 1; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Match(locator))
                    {
                        // Remove from the set
                        _references.RemoveAt(index);
                        return reference.GetComponent();
                    }
                }
            }

            return null;
        }

        public virtual List<object> RemoveAll(object locator)
        {
            var components = new List<object>();

            lock (_lock)
            {
                for (int index = _references.Count - 1; index >= 0; index--)
                {
                    var reference = _references[index];
                    if (reference.Match(locator))
                    {
                        // Remove from the set
                        _references.RemoveAt(index);
                        components.Add(reference.GetComponent());
                    }
                }
            }

            return components;
        }

        public virtual List<object> GetAll()
        {
            var components = new List<object>();
            foreach (var reference in _references)
                components.Add(reference.GetComponent());
            return components;
        }

        public virtual object GetOneOptional(object locator)
        {
            var components = Find<object>(new ReferenceQuery(locator), false);
            return components.Count > 0 ? components[0] : null;
        }

        public virtual T GetOneOptional<T>(object locator)
        {
            var components = Find<T>(new ReferenceQuery(locator), false);
            return components.Count > 0 ? components[0] : default(T);
        }

        public virtual object GetOneRequired(object locator)
        {
            var components = Find<object>(new ReferenceQuery(locator), true);
            return components.Count > 0 ? components[0] : null;
        }

        public virtual T GetOneRequired<T>(object locator)
        {
            var components = Find<T>(new ReferenceQuery(locator), true);
            return components.Count > 0 ? components[0] : default(T);
        }

        public virtual List<object> GetOptional(object locator)
        {
            return Find<object>(new ReferenceQuery(locator), false);
        }

        public virtual List<T> GetOptional<T>(object locator)
        {
            return Find<T>(new ReferenceQuery(locator), false);
        }

        public virtual List<object> GetRequired(object locator)
        {
            return Find<object>(new ReferenceQuery(locator), true);
        }

        public virtual List<T> GetRequired<T>(object locator)
        {
            return Find<T>(new ReferenceQuery(locator), true);
        }

        public virtual List<object> Find(ReferenceQuery query, bool required)
        {
            return Find<object>(query, required);
        }

        public virtual List<T> Find<T>(ReferenceQuery query, bool required)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var components = new List<T>();

            lock (_lock)
            {
                var index = query.Ascending ? 0 : _references.Count - 1;

                // Locate the start
                if (query.StartLocator != null)
                {
                    while (index >= 0 && index < _references.Count)
                    {
                        var reference = _references[index];
                        if (reference.Match(query.StartLocator))
                            break;
                        index += query.Ascending ? 1 : -1;
                    }
                }

                // Search all references
                while (index >= 0 && index < _references.Count)
                {
                    var reference = _references[index];
                    if (reference.Match(query.Locator))
                    {
                        var component = reference.GetComponent();
                        if (component is T)
                            components.Add((T)component);
                    }
                    index += query.Ascending ? 1 : -1;
                }
            }

            if (components.Count == 0 && required)
                throw new ReferenceException(null, query.Locator);

            return components;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _references.Clear();
            }
        }

        public static References FromTuples(params object[] tuples)
        {
            return new References(tuples);
        }
    }
}