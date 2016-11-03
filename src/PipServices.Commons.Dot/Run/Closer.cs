using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class Closer
    {
        public static async Task CloseAsync(string correlationId, IEnumerable<object> components, CancellationToken token)
        {
            if (components == null) return;

            foreach(var component in components)
            {
                var closable = component as IClosable;
                if(closable != null)
                {
                    await closable.CloseAsync(correlationId, token);
                }
            }
        }
    }
}
