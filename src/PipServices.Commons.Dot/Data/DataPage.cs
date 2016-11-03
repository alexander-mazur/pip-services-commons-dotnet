using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PipServices.Commons.Data
{
    [DataContract]
    public class DataPage<T>
    {
        public DataPage() { }

        public DataPage(List<T> data, long? total = null)
        {
            if (data != null)
            {
                Data = data;
            }
            Total = total;
        }

        [DataMember]
        public long? Total { get; set; }

        [DataMember]
        public List<T> Data { get; set; } = new List<T>();
    }
}