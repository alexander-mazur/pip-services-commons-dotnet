using PipServices.Commons.Run;
using System.Collections;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class ManagedReferences: ReferencesDecorator, IOpenable, IClosable
    {
        protected References _references;
        protected BuildReferencesDecorator _builder;
        protected LinkReferencesDecorator _linker;
        protected RunReferencesDecorator _runner;

        public ManagedReferences(IEnumerable components = null)
        {
            _references = new References(components);
            _builder = new BuildReferencesDecorator(_references, this);
            _linker = new LinkReferencesDecorator(_builder, this);
            _runner = new RunReferencesDecorator(_linker, this);

            BaseReferences = _runner;
        }

        public async Task OpenAsync(string correlationId)
        {
            var components = _references.GetAll();
            Referencer.SetReferencesForComponents(this, components);
            await Opener.OpenComponentsAsync(correlationId, components);
        }

        /// <summary>
        /// close all references as an asynchronous operation.
        /// </summary>
        /// <param name="correlationId">a unique transaction id to trace calls across components</param>
        /// <returns>Task.</returns>
        public async Task CloseAsync(string correlationId)
        {
            var components = _references.GetAll();
            await Closer.CloseComponentsAsync(correlationId, components);
            Referencer.UnsetReferencesForComponents(components);

            _references.Clear();
        }
    }
}
