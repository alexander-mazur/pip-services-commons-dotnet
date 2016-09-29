using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class Notifier
    {
        public async Task NotifyAsync(string correlationId, IEnumerable<object> components)
        {
            await NotifyAsync(correlationId, components, new Parameters());
        }

        public async Task NotifyAsync(string correlationId, IEnumerable<object> components, Parameters args)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var notifiable = component as INotifiable;

                if (notifiable != null)
                {
                    await notifiable.NotifyAsync(correlationId);
                    continue;
                }

                var paramNotifiable = component as IParamNotifiable;
                if (paramNotifiable != null)
                {
                    await paramNotifiable.NotifyAsync(correlationId, args);
                }
            }
        }
    }
}