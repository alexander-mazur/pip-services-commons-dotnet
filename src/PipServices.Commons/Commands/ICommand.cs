using System.Collections.Generic;
using PipServices.Commons.Validation;
using PipServices.Commons.Run;

namespace PipServices.Commons.Commands
{
    public interface ICommand : IParamExecutable
    {
        string Name { get; }
        List<ValidationException> Validate(Parameters args);
    }
}