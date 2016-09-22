using System;
using System.Runtime.Serialization;

namespace PipServices.Commons.Data
{
    [DataContract]
    public class PagingParams
    {
        public PagingParams(int skip = 0, int take = 100, bool total = true)
        {
            Skip = skip;
            Take = take;
            Total = total;
        }

        [DataMember]
        public int Skip { get; set; }

        [DataMember]
        public int Take { get; set; }

        [DataMember]
        public bool Total { get; set; }

        public int GetSkip()
        {
            return Math.Max(0, Skip);
        }

        public int GetTake(int maxTake)
        {
            return Math.Max(0, Math.Min(Take, maxTake));
        }
    }
}