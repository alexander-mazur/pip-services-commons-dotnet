namespace PipServices.Commons.Refer
{
    public class ReferenceQuery
    {
        public ReferenceQuery()
        {
            Ascending = false;
        }

        public ReferenceQuery (object locator, object startLocator = null, bool ascending = false)
        {
            Locator = locator;
            StartLocator = startLocator;
            Ascending = ascending;
        }

        public object Locator { get; set; }
        public object StartLocator { get; set; }
        public bool Ascending { get; set; }
    }
}
