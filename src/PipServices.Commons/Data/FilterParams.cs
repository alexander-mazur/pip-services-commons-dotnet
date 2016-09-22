using System.Collections.Generic;

namespace PipServices.Commons.Data
{
    public class FilterParams : StringValueMap
    {
        public FilterParams() { }
        public FilterParams(IDictionary<string, string> values)
        {
            if (values != null)
            {
                foreach (var entry in values)
                    Set(entry.Key, entry.Value);
            }
        }

        public FilterParams(IDictionary<string, object> values)
        {
            if (values != null)
            {
                foreach (var entry in values)
                    Set(entry.Key, entry.Value);
            }
        }

        public FilterParams(params object[] values)
        {
            if (values != null)
                SetTuples(values);
        }

        public static FilterParams From(params object[] values)
        {
            var result = new FilterParams();
            result.SetTuples(values);
            return result;
        }
    }
}
