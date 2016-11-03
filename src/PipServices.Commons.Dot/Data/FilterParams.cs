using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Data
{
    public class FilterParams : StringValueMap
    {
        public FilterParams() { }

        public FilterParams(IDictionary<string, object> map)
        {
            SetAsMap(map);
        }

        public new static FilterParams FromTuples(params object[] values)
        {
            var map = StringValueMap.FromTuples(values);
            return new FilterParams(map);
        }

        public new static FilterParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new FilterParams(map);
        }
    }
}