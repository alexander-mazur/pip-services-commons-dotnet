using System;
using PipServices.Commons.Data;
using Xunit;

namespace PipServices.Commons.Test.Data
{
    public sealed class IdGeneratorTest
    {
        private void TestIds(Func<string> generator, int minSize)
        {
            var id1 = generator();
            Assert.NotNull(id1);
            Assert.True(id1.Length >= minSize);

            var id2 = generator();
            Assert.NotNull(id2);
            Assert.True(id2.Length >= minSize);
            Assert.NotEqual(id1, id2);
        }

        [Fact]
        public void TestShortId()
        {
            TestIds(IdGenerator.NextShort, 9);
        }

        [Fact]
        public void TestLong()
        {
            TestIds(IdGenerator.NextLong, 32);
        }
    }
}
