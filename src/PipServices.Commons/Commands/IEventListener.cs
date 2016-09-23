using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    public interface IEventListener
    {
        void OnEvent(IEvent e, string correlationId, Parameters value);
    }
}
