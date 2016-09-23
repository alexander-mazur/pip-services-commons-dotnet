using System;
using System.Collections.Generic;
using PipServices.Commons.Run;
using PipServices.Commons.Validation;
using PipServices.Commons.Errors;

namespace PipServices.Commons.Commands
{
    public class Command : ICommand
    {
        public string Name { get; }
        private Schema _schema;
        private IParamExecutable _function;

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

        public List<ValidationException> Validate(Parameters args)
        {
            if(_schema == null)
            {
                return new List<ValidationException>();
            }

            // TODO: complete implementation
            return new List<ValidationException>();
        }

        public object Execute(string correlationId, Parameters args)
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
                return _function.Execute(correlationId, args);
            }
            catch (Exception ex)
            {
                throw new InvocationException(
                    correlationId, "EXEC_FAILED", "Execution " + Name + " failed: " + ex, ex)
                    .WithDetails(Name);
            }
        }
    }
}
