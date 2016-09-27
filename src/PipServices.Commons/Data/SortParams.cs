using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Data
{
    public class SortParams : List<SortField>
    {
        public SortParams(IEnumerable<SortField> fields = null)
        {
            if (fields != null)
            {
                AddRange(fields);
            }
        }
    }
}