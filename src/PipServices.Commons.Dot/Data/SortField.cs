namespace PipServices.Commons.Data
{
    public class SortField
    {
        public string Name { get; set; }
        public bool Ascending { get; set; } = true;

        public SortField(string name = null, bool ascending = true)
        {
            Name = name;
            Ascending = ascending;
        }
    }
}
