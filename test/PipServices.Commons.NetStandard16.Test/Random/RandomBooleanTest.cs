﻿using Xunit;

namespace PipServices.Commons.Random
{
    public class RandomBooleanTest
    {
        [Fact]
        public void TestChance()
        {
            bool value;
            value = RandomBoolean.Chance(5, 10);
            Assert.True(value || !value);

            value = RandomBoolean.Chance(5, 5);
            Assert.True(value || !value);

            value = RandomBoolean.Chance(0, 0);
            Assert.True(!value);

            value = RandomBoolean.Chance(-1, 0);
            Assert.True(!value);

            value = RandomBoolean.Chance(-1, -1);
            Assert.True(!value);
        }
    }
}
