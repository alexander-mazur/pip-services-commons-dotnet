using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Interface for components that require explicit opening
    /// </summary>
    public interface IOpenable
    {
        /// <summary>
        /// Opens component, establishes connections to services
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        Task OpenAsync(string correlationId);
    }
}
