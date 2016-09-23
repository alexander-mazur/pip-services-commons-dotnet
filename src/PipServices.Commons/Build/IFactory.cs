using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    public interface IFactory
    {
        bool CanCreate(Descriptor descriptor);
        object Create(Descriptor descriptor);
    }
}
