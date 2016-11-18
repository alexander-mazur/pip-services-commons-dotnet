using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface ICleanable
    {
        Task ClearAsync(string correlationId, CancellationToken token);
    }
}
