using System;
using System.Collections.Generic;
using PipServices.Commons.Run;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Commands
{
    public class Event : IEvent
    {
        public List<IEventListener> Listeners { get; } = new List<IEventListener>();

        public Event(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public string Name { get; }

        public void AddListener(IEventListener listener)
        {
            Listeners.Add(listener);
        }

        public void RemoveListener(IEventListener listener)
        {
            Listeners.Remove(listener);
        }

        public void Notify(string correlationId, Parameters args)
        {
            foreach (var listener in Listeners)
            {
                try
                {
                    listener.OnEvent(this, correlationId, args);
                }
                catch (Exception ex)
                {
                    throw new InvocationException(
                        correlationId,
                        "EXEC_FAILED",
                        "Raising event " + Name + " failed: " + ex, ex)
                        .WithDetails(Name);
                }
            }
        }
    }
}