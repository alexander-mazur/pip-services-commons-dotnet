using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class ReferenceItem
    {
        public Locator Locator { get; }
        public object Reference { get; }

        public ReferenceItem(Locator locator, object reference)
        {
            Locator = locator;
            Reference = reference;
        }
    }
}
