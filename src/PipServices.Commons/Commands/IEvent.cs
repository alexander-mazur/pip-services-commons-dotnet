using System.Collections.Generic;
using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    public interface IEvent : IParamNotifiable
    {
        string Name { get; }
        List<IEventListener> Listeners { get; }
        void AddListener(IEventListener listener);
        void RemoveListener(IEventListener listener);
    }
}
