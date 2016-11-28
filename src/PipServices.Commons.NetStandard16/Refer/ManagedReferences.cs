using PipServices.Commons.Run;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class ManagedReferences: ReferencesDecorator, IOpenable, IClosable
    {
        private References _references;
        private BuildReferencesDecorator _builder;
        private LinkReferencesDecorator _linker;
        private RunReferencesDecorator _runner;

        public ManagedReferences(params object[] components)
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

        public async Task CloseAsync(string correlationId)
        {
            var components = _references.GetAll();
            await Closer.CloseComponentsAsync(correlationId, components);
            Referencer.UnsetReferencesForComponents(components);
        }
    }
}
