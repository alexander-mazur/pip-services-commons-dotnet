namespace PipServices.Commons.Config
{
    public abstract class FileConfigReader: CachedConfigReader, IConfigurable
    {
        public FileConfigReader(string name = null, string path = null)
            : base(name)
        {
            Path = path;
        }

        public string Path { get; set; }

        public override void Configure(ConfigParams config)
        {
            Path = config.GetAsString("path");
        }
    }
}
