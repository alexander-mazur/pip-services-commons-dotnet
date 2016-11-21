using System.Collections;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Helper class that opens a collection of components 
    /// </summary>
    public class Opener
    {
        /// <summary>
        /// Opens component that implement IOpenable interface
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="components">a list of components to be opened</param>
        public static async Task OpenAsync(string correlationId, IEnumerable components)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var openable = component as IOpenable;
                if (openable != null)
                    await openable.OpenAsync(correlationId);
            }
        }
    }
}
