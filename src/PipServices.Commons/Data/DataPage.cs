using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PipServices.Commons.Data
{
    [DataContract]
    public class DataPage<T>
    {
        private long? _total;
        private List<T> _data = new List<T>();

        public DataPage() { }

        public DataPage(List<T> data, long? total = null)
        {
            _data = data ?? new List<T>();
            _total = total;
        }

        [DataMember]
        public long? Total
        {
            get { return _total; }
            set { _total = value; }
        }

        [DataMember]
        public List<T> Data
        {
            get { return _data; }
            set { _data = value ?? new List<T>(); }
        }
    }
}
