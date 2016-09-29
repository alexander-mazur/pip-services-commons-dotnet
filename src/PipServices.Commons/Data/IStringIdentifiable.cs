namespace PipServices.Commons.Data
{
    public interface IStringIdentifiable : IIdentifiable<string>
    {
        new string Id { get;  set; }
    }
}
