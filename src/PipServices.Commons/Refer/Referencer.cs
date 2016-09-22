using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class Referencer
    {
        public static void SetReferences(IReferences references, IEnumerable<object> components)
        {
            foreach (var c in components)
            {
                var referenceale = c as IReferenceable;
                if (referenceale != null)
                {
                    referenceale.SetReferences(references);
                }
            }
        }

        public static void UnsetReferences(IEnumerable<object> components)
        {
            foreach (var c in components)
            {
                var unreferenceale = c as IUnreferenceable;
                if (unreferenceale != null)
                {
                    unreferenceale.UnsetReferences();
                }
            }
        }
    }
}