using PipServices.Commons.Refer;

namespace PipServices.Commons.Build
{
    public interface IFactory
    {
        bool CanCreate(Locator locator);
        object Create();
    }
}
