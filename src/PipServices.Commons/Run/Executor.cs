using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    public class Executor
    {
        public Task<List<object>> ExecuteAsync(string correlationId, IEnumerable<object> components, CancellationToken token)
        {
            return ExecuteAsync(correlationId, components, new Parameters(), token);
        }

        public async Task<List<object>> ExecuteAsync(string correlationId, IEnumerable<object> components, Parameters args, CancellationToken token)
        {
            var results = new List<object>();
            if (components == null) return results;

            foreach (var component in components)
            {
                var executable = component as IExecutable;
                if (executable != null)
                {
                    results.Add(await executable.ExecuteAsync(correlationId, token));
                    continue;
                }

                var paramExecutable = component as IParamExecutable;
                if (paramExecutable != null)
                {
                    results.Add(await paramExecutable.ExecuteAsync(correlationId, args, token));
                }
            }

            return results;
        }
    }
}