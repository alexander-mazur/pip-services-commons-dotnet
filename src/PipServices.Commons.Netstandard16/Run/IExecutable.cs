using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Interface for active components that can called to execute work.
    /// In contrast to IParamExecutable this interface does not require parameters
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Executes a unit of work
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <returns>execution result</returns>
        Task<object> ExecuteAsync(string correlationId);
    }
}
