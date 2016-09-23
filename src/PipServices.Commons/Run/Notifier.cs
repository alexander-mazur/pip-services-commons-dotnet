using System.Collections.Generic;

namespace PipServices.Commons.Run
{
    public class Notifier
    {
        public void Notify(string correlationId, IEnumerable<object> components)
        {
            Notify(correlationId, components, new Parameters());
        }

        public void Notify(string correlationId, IEnumerable<object> components, Parameters args)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var notifiable = component as INotifiable;

                if (notifiable != null)
                {
                    notifiable.Notify(correlationId);
                    continue;
                }

                var paramNotifiable = component as IParamNotifiable;
                if (paramNotifiable != null)
                {
                    paramNotifiable.Notify(correlationId, args);
                }
            }
        }
    }
}