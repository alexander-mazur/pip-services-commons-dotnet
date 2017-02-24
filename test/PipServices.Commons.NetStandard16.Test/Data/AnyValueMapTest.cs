using System.Threading;
using System.Threading.Tasks;
using PipServices.Commons.Convert;
using Xunit;

namespace PipServices.Commons.Data
{
    public class AnyValueMapTest
    {
        [Fact]
        public void TestGetAsString()
        {
            var message = AnyValueMap.FromTuples(
                "key1", 123,
                "key2", "ABC"
            );

            var restoredMessageObject = JsonConverter.FromJson<object>(JsonConverter.ToJson(message));
            var restoredMessage = AnyValueMap.FromValue(restoredMessageObject);

            Assert.Equal(2, restoredMessage.Count);
            Assert.Equal(123, restoredMessage.GetAsInteger("key1"));
            Assert.Equal("ABC", restoredMessage.GetAsString("key2"));
        }
    }
}
