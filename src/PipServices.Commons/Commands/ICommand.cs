using System.Collections.Generic;
using PipServices.Commons.Validation;
using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    /// <summary>
    /// Interface for commands that execute functional operations.
    /// </summary>
    public interface ICommand : IParamExecutable
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Performs validation of the command arguments.
        /// </summary>
        /// <param name="args">Command arguments.</param>
        /// <returns>List of errors or empty list if validation was successful.</returns>
        List<ValidationException> Validate(Parameters args);
    }
}