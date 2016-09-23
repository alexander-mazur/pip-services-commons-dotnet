using System.Collections.Generic;

namespace PipServices.Commons.Run
{
    public class Executor
    {
        public List<object> Execute(string correlationId, IEnumerable<object> components)
        {
            return Execute(correlationId, components, new Parameters());
        }

        public List<object> Execute(string correlationId, IEnumerable<object> components, Parameters args)
        {
            var results = new List<object>();
            if (components == null) return results;

            foreach (var component in components)
            {
                var executable = component as IExecutable;
                if (executable != null)
                {
                    results.Add(executable.Execute(correlationId));
                    continue;
                }

                var paramExecutable = component as IParamExecutable;
                if (paramExecutable != null)
                {
                    results.Add(paramExecutable.Execute(correlationId, args));
                }
            }

            return results;
        }
    }
}