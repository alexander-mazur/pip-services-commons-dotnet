using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class Opener
    {
        public static async Task OpenAsync(string correlationId, IEnumerable<object> components, CancellationToken token)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var openable = component as IOpenable;
                if (openable != null)
                {
                    await openable.OpenAsync(correlationId, token);
                }
            }
        }
    }
}
