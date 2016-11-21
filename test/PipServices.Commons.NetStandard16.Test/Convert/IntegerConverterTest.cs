﻿using Xunit;

namespace PipServices.Commons.Convert
{
    public class IntegerConverterTest
    {
        [Fact]
        public void TestToInteger()
        {
            Assert.Equal(123, IntegerConverter.ToInteger(123));
            Assert.Equal(123, IntegerConverter.ToInteger(123.456));
            Assert.Equal(123, IntegerConverter.ToInteger("123"));
            Assert.Equal(123, IntegerConverter.ToInteger("123.465"));

            Assert.Equal(123, IntegerConverter.ToIntegerWithDefault(null, 123));
            Assert.Equal(0, IntegerConverter.ToIntegerWithDefault(false, 123));
            Assert.Equal(123, IntegerConverter.ToIntegerWithDefault("ABC", 123));
        }
    }
}
