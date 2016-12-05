namespace PipServices.Commons.Data
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}