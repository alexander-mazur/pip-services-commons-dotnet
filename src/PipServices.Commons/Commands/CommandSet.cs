using System.Collections.Generic;
using PipServices.Commons.Data;
using PipServices.Commons.Errors;
using PipServices.Commons.Run;
using PipServices.Commons.Validation;
using System.Linq;

namespace PipServices.Commons.Commands
{
    public class CommandSet
    {
        private List<ICommand> Commands { get; } = new List<ICommand>();
        private List<IEvent> Events { get; } = new List<IEvent>();

        private Dictionary<string, ICommand> _commandsByName = new Dictionary<string, ICommand>();
        private Dictionary<string, IEvent> _eventsByName = new Dictionary<string, IEvent>();
        private List<ICommandIntercepter> _intercepters = new List<ICommandIntercepter>();

        public ICommand FindCommand(string command)
        {
            ICommand value;
            _commandsByName.TryGetValue(command, out value);
            return value;
        }

        public IEvent FindEvent(string ev)
        {
            IEvent value;
            _eventsByName.TryGetValue(ev, out value);
            return value;
        }

        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
            BuildCommandChain(command);
        }

        public void AddCommands(IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                AddCommand(command);
            }
        }

        public void AddEvent(IEvent ev)
        {
            Events.Add(ev);
            _eventsByName[ev.Name] = ev;
        }

        public void AddEvents(IEnumerable<IEvent> events)
        {
            foreach (var ev in events)
            {
                AddEvent(ev);
            }
        }

        public void AddCommandSet(CommandSet commands)
        {
            foreach (var command in commands.Commands)
            {
                AddCommand(command);
            }
        }

        public void AddInterceptor(ICommandIntercepter interceptor)
        {
            _intercepters.Add(interceptor);
            RebuildAllCommandChains();
        }

        public object Execute(string correlationId, string command, Parameters args)
        {
            var cref = FindCommand(command);
            if (cref == null)
            {
                throw new BadRequestException(
                    correlationId,
                    "CMD_NOT_FOUND",
                    "Request command does not exist")
                    .WithDetails(command);
            }

            if (correlationId == null)
            {
                correlationId = IdGenerator.NextShort();
            }

            var errors = cref.Validate(args);
            if (errors.Count > 0)
            {
                throw errors[0];
            }

            return cref.Execute(correlationId, args);
        }

        public List<ValidationException> Validate(string command, Parameters args)
        {
            var cref = FindCommand(command);
            if (cref == null)
            {
                var errors = new List<ValidationException>();
                errors.Add((ValidationException)new ValidationException(
                    "CMD_NOT_FOUND",
                    "Request command does not exist")
                    .WithDetails(command));
                return errors;
            }

            return cref.Validate(args);
        }

        public void AddListener(IEventListener listener)
        {
            foreach (var ev in Events)
            {
                ev.AddListener(listener);
            }
        }

        public void RemoveListener(IEventListener listener)
        {
            foreach (var ev in Events)
            {
                ev.RemoveListener(listener);
            }
        }

        public void Notify(string ev, string correlationId, Parameters args)
        {
            var e = FindEvent(ev);
            if (e != null)
            {
                e.Notify(correlationId, args);
            }
        }

        private void BuildCommandChain(ICommand command)
        {
            var next = command;
            for (var i = _intercepters.Count - 1; i >= 0; i--)
            {
                next = new InterceptedCommand(_intercepters[i], next);
            }
            _commandsByName[next.Name] = next;
        }


        private void RebuildAllCommandChains()
        {
            _commandsByName.Clear();
            foreach (var command in Commands)
            {
                BuildCommandChain(command);
            }
        }
    }
}