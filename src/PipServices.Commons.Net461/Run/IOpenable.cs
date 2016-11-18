using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface IOpenable
    {
        Task OpenAsync(string correlationId, CancellationToken token);
    }
}
