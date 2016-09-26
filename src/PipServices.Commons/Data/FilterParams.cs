using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Data
{
    public class FilterParams : StringValueMap
    {
        public FilterParams() { }

        public FilterParams(IDictionary map)
        {
            SetAsMap(map);
        }

        public static new FilterParams FromTuples(params object[] values)
        {
            var map = StringValueMap.FromTuples(values);
            return new FilterParams(map);
        }

        public static new FilterParams FromString(string line)
        {
            var map = StringValueMap.FromString(line);
            return new FilterParams(map);
        }
    }
}
