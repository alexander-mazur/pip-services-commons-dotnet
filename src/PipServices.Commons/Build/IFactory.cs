using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    /// <summary>
    /// Factory interface for factories that understand <see cref="Descriptor"/>.
    /// </summary>
    public interface IFactory
    {
        bool CanCreate(Descriptor descriptor);
        object Create(Descriptor descriptor);
    }
}