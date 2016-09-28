namespace PipServices.Commons.Data
{
    public interface IStringIdentifiable : IIdentifiable<string>
    {
        void SetId(string value);
    }
}
