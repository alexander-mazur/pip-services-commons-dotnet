namespace PipServices.Commons.Build
{
    public interface IFactory
    {
        bool CanCreate(object locator);
        object Create(object locator);
    }
}