using PipServices.Commons.Convert;
using System;
using System.Runtime.Serialization;

namespace PipServices.Commons.Data
{
    [DataContract]
    public class PagingParams
    {
        public PagingParams() {}

        public PagingParams(object skip, object take, object total)
        {
            Skip = IntegerConverter.ToNullableInteger(skip);
            Take = IntegerConverter.ToNullableInteger(take);
            Total = BooleanConverter.ToBooleanWithDefault(total, false);
        }

        [DataMember]
        public int? Skip { get; set; }

        [DataMember]
        public int? Take { get; set; }

        [DataMember]
        public bool Total { get; set; }

        public int GetSkip(int minSkip)
        {
            if (Skip == null) return minSkip;
            if (Skip.Value < minSkip) return minSkip;
            return Skip.Value;
        }

        public int GetTake(int maxTake)
        {
            if (Take == null) return maxTake;
            if (Take.Value < 0) return 0;
            if (Take.Value > maxTake) return maxTake;
            return Take.Value;
        }

        public static PagingParams FromValue(object value)
        {
            if (value is PagingParams)
            {
                return (PagingParams)value;
            }
            var map = AnyValueMap.FromValue(value);
            return FromMap(map);
        }

        public static PagingParams FromTuples(params object[] tuples)
        {
            var map = AnyValueMap.FromTuples(tuples);
            return FromMap(map);
        }

        public static PagingParams FromMap(AnyValueMap map)
        {
            var skip = map.GetAsNullableInteger("skip");
            var take = map.GetAsNullableInteger("take");
            var total = map.GetAsBooleanWithDefault("total", true);
            return new PagingParams(skip, take, total);
        }
    }
}