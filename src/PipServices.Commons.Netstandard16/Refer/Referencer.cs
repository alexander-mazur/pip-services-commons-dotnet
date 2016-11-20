using System.Collections;

namespace PipServices.Commons.Refer
{
    /// <summary>
    /// Helper class that assigns references to components
    /// </summary>
    public class Referencer
    {
        /// <summary>
        /// Assigns references to components that implement IReferenceable interface
        /// </summary>
        /// <param name="references">references to be assigned</param>
        /// <param name="components">a list of components to assign references</param>
        public static void SetReferences(IReferences references, IEnumerable components)
        {
            foreach (var component in components)
            {
                var referenceale = components as IReferenceable;
                if (referenceale != null)
                    referenceale.SetReferences(references);
            }
        }

        /// <summary>
        /// Clears references for components that implement IUnreferenceable interface 
        /// </summary>
        /// <param name="components">a list of components to clear references</param>
        public static void UnsetReferences(IEnumerable components)
        {
            foreach (var component in components)
            {
                var unreferenceale = component as IUnreferenceable;
                if (unreferenceale != null)
                    unreferenceale.UnsetReferences();
            }
        }
    }
}