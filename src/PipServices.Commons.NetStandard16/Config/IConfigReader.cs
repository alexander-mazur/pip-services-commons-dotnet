namespace PipServices.Commons.Config
{
    public interface IConfigReader
    {
        ConfigParams ReadConfig(string correlationId);
    }
}
