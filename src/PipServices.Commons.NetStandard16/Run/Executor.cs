using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServices.Commons.Run
{
    /// <summary>
    /// Helper class that triggers execution for components
    /// </summary>
    public class Executor
    {
        /// <summary>
        /// Triggers execution for components that implement IExecutable and IParamExecutable interfaces.
        /// IParamExecutable components receive an empty parameter set
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="components">a list of components to be executed</param>
        /// <returns>execution results</returns>
        public static async Task<List<object>> ExecuteComponentsAsync(string correlationId, IEnumerable components)
        {
            return await ExecuteComponentsAsync(correlationId, components, new Parameters());
        }

        /// <summary>
        /// Triggers execution for components that implement IExecutable and IParamExecutable interfaces
        /// and passes to IParamExecutable them set of parameters.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <param name="components">a list of components to be executed</param>
        /// <param name="args">a set of parameters to pass to executed components</param>
        /// <returns>execution results</returns>
        public static async Task<List<object>> ExecuteComponentsAsync(
            string correlationId, IEnumerable components, Parameters parameters)
        {
            var results = new List<object>();
            if (components == null) return results;

            foreach (var component in components)
            {
                var executable = component as IExecutable;
                if (executable != null)
                {
                    results.Add(await executable.ExecuteAsync(correlationId));
                }
                else
                {
                    var paramExecutable = component as IParamExecutable;
                    if (paramExecutable != null)
                        results.Add(await paramExecutable.ExecuteAsync(correlationId, parameters));
                }
            }

            return results;
        }
    }
}