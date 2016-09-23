using System;
using System.Collections.Generic;
using PipServices.Commons.Data;

namespace PipServices.Commons.Run
{
    public class Parameters : AnyValueMap
    {
        public Parameters()
        { }

        public Parameters(IDictionary<string, object> values)
        {
            if (values != null)
            {
                foreach (var entry in values)
                    Set(entry.Key, entry.Value);
            }
        }

        public Parameters(params object[] values)
        {
            SetTuples(values);
        }

        public Parameters(IDictionary<string, string> values)
        {
            if (values != null)
            {
                foreach (var entry in values)
                    Set(entry.Key, entry.Value);
            }
        }

        public override object Get(string key)
        {
            object value = TryGet(key);
            if (value == null)
            {
                throw new NullReferenceException("Parameter " + key + " is not defined");
            }
            return value;
        }

        public Parameters Merge(IDictionary<string, object> values)
        {
            var result = new Parameters(this);

            if (values != null)
            {
                foreach (var entry in values)
                {
                    result.Set(entry.Key, entry.Value);
                }
            }

            return result;
        }

        public static Parameters From(params object[] values)
        {
            return new Parameters(values);
        }

        public static Parameters From(IDictionary<string, string> values)
        {
            return new Parameters(values);
        }
    }
}