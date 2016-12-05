using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Helper class that triggers notification for components
    /// </summary>
    public class Notifier
    {
        /// <summary>
        /// Triggers notification for components that implement INotifiable and IParamNotifiable interfaces.
        /// IParamNotifiable components receive an empty parameter set
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="components">a list of components to be notified</param>
        public static async Task NotifyAsync(string correlationId, IEnumerable components)
        {
            await NotifyAsync(correlationId, components, new Parameters());
        }

        /// <summary>
        /// Triggers notification for components that implement INotifiable and IParamParam interfaces
        /// and passes to IParamNotifiable them set of parameters.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="components">a list of components to be notified</param>
        /// <param name="args">a set of parameters to pass to notified components</param>
        public static async Task NotifyAsync(string correlationId, IEnumerable components, Parameters args)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var notifiable = component as INotifiable;

                if (notifiable != null)
                {
                    await notifiable.NotifyAsync(correlationId);
                }
                else
                {
                    var paramNotifiable = component as IParamNotifiable;
                    if (paramNotifiable != null)
                        await paramNotifiable.NotifyAsync(correlationId, args);
                }
            }
        }
    }
}