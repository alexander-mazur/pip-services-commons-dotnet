using System.Collections.Generic;
using PipServices.Commons.Validation;
using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    public interface ICommandIntercepter
    {
        string GetName(ICommand command);
        object Execute(string correlationId, ICommand command, Parameters args);
        List<ValidationException> Validate(ICommand command, Parameters args);
    }
}
