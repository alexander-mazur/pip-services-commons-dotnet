namespace PipServices.Commons.Config
{
    public abstract class FileConfigReader: IConfigReader, IConfigurable
    {
        public FileConfigReader(string path = null)
        {
            Path = path;
        }

        public string Path { get; set; }

        public virtual void Configure(ConfigParams config)
        {
            Path = config.GetAsString("path");
        }

        public abstract ConfigParams ReadConfig(string correlationId);
    }
}
