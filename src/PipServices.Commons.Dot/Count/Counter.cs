using System;
using Newtonsoft.Json;

namespace PipServices.Commons.Count
{
    public sealed class Counter
    {
        public Counter()
        {
        }

        public Counter(string name, CounterType type)
        {
            Name = name;
            Type = type;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public CounterType? Type { get; set; }

        [JsonProperty("last")]
        public double? Last { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("min")]
        public double? Min { get; set; }

        [JsonProperty("max")]
        public double? Max { get; set; }

        [JsonProperty("average")]
        public double? Average { get; set; }

        [JsonProperty("time")]
        public DateTime? Time { get; set; }
    }
}
