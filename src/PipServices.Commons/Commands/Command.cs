using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PipServices.Commons.Run;
using PipServices.Commons.Validation;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Commands
{
    /// <summary>
    /// Represents a command that implements a command pattern.
    /// </summary>
    public class Command : ICommand
    {
        public string Name { get; }
        private readonly Schema _schema;
        private readonly IParamExecutable _function;

        /// <summary>
        /// Creates an instance of Command.
        /// </summary>
        /// <param name="name">Name of the command.</param>
        /// <param name="schema">Schema for command arguments.</param>
        /// <param name="function">Execution function wrapped in this command.</param>
        public Command(string name, Schema schema, IParamExecutable function)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (function == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
            _schema = schema;
            _function = function;
        }

        /// <summary>
        /// Performs validation of the command arguments.
        /// </summary>
        /// <param name="args">Cimmand arguments.</param>
        /// <returns>A list of errors or empty list if validation was successful.</returns>
        public List<ValidationException> Validate(Parameters args)
        {
            if (_schema == null)
            {
                return new List<ValidationException>();
            }

            // TODO: complete implementation
            return new List<ValidationException>();
        }

        /// <summary>
        /// Executes the command given specific arguments as input.
        /// </summary>
        /// <param name="correlationId">Unique correlation/transaction id.</param>
        /// <param name="args">Command arguments.</param>
        /// <param name="token"></param>
        /// <returns>Execution result.</returns>
        public async Task<object> ExecuteAsync(string correlationId, Parameters args, CancellationToken token)
        {
            if (_schema != null)
            {
                var errors = Validate(args);
                if (errors.Count > 0)
                {
                    throw errors[0];
                }
            }

            try
            {
                return await _function.ExecuteAsync(correlationId, args, token);
            }
            catch (Exception ex)
            {
                throw new InvocationException(
                    correlationId, "EXEC_FAILED", "Execution " + Name + " failed: " + ex, ex)
                    .WithDetails("command", Name);
            }
        }
    }
}