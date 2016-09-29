using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PipServices.Commons.Validation;
using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    /// <summary>
    /// Interface for stackable command intercepters
    /// </summary>
    public interface ICommandIntercepter
    {
        /// <summary>
        /// Gets the command name. Intercepter can modify the name if needed.
        /// </summary>
        /// <param name="command">The intercepted command.</param>
        /// <returns>Command name.</returns>
        string GetName(ICommand command);

        /// <summary>
        /// Executes the command given specific arguments as input.
        /// </summary>
        /// <param name="correlationId">Unique correlation/transaction id.</param>
        /// <param name="command">Intercepted command.</param>
        /// <param name="args">Map with command arguments.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<object> ExecuteAsync(string correlationId, ICommand command, Parameters args, CancellationToken token);

        /// <summary>
        /// Performs validation of the command arguments.
        /// </summary>
        /// <param name="command">Intercepted command.</param>
        /// <param name="args">Command arguments.</param>
        /// <returns>A list of errors or an empty list if validation was successful.</returns>
        List<ValidationException> Validate(ICommand command, Parameters args);
    }
}