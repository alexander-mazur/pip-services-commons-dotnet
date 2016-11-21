namespace PipServices.Commons.Data
{
    public interface IStringIdentifiable : IIdentifiable<string>
    {
        /// <summary>
        ///  Sets the object id
        /// </summary>
        new string Id { get;  set; }
    }
}
