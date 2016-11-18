using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class Cleaner
    {
        public static async Task ClearAsync(string correlationId, IEnumerable components, CancellationToken token)
        {
            if (components == null)
                return;

            foreach (var component in  components)
            {
                var cleanable = component as ICleanable;
                if (cleanable == null)
                    continue;

                await cleanable.ClearAsync(correlationId, token);
            }
        }
    }
}
