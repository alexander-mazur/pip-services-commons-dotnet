using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface INotifiable
    {
        Task NotifyAsync(string correlationId);
    }
}