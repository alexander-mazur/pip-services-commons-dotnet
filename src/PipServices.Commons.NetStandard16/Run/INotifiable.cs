using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Interface for active components that can be notified (called without expecting a result).
    /// In contrast to IParamNotifiable this interface does not require parameters
    /// </summary>
    public interface INotifiable
    {
        /// <summary>
        /// Executes a unit of work
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        Task NotifyAsync(string correlationId);
    }
}