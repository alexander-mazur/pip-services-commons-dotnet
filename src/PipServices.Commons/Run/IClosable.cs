using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public interface IClosable
    {
        Task CloseAsync(string correlationId);
    }
}
