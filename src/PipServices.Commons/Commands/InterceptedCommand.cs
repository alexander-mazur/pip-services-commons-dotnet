using System.Collections.Generic;
using PipServices.Commons.Run;
using PipServices.Commons.Validation;

namespace PipServices.Commons.Commands
{
    public class InterceptedCommand : ICommand
    {
        private ICommandIntercepter _intercepter;
        private ICommand _next;

        public InterceptedCommand(ICommandIntercepter intercepter, ICommand next)
        {
            _intercepter = intercepter;
            _next = next;
        }

        public string Name
        {
            get { return _intercepter.GetName(_next); }
        }

        public object Execute(string correlationId, Parameters args)
        {
            return _intercepter.Execute(correlationId, _next, args);
        }

        public List<ValidationException> Validate(Parameters args)
        {
            return _intercepter.Validate(_next, args);
        }
    }
}
