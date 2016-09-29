using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface IParamNotifiable
    {
        Task NotifyAsync(string correlationId, Parameters args);
    }
}
