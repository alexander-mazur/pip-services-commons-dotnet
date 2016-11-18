using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface IExecutable
    {
        Task<object> ExecuteAsync(string correlationId, CancellationToken token);
    }
}
