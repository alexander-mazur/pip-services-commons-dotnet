using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface IParamExecutable
    {
        Task<object> ExecuteAsync(string correlationId, Parameters args, CancellationToken token);
    }
}
