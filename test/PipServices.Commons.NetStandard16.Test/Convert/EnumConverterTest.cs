﻿using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Convert
{
    public class EnumConverterTest
    {
        [Fact]
        public void TestToEnum()
        {
            Assert.Equal(LogLevel.None, EnumConverter.ToEnum<LogLevel>("ABC"));
            Assert.Equal(LogLevel.Fatal, EnumConverter.ToEnum<LogLevel>(1));
            Assert.Equal(LogLevel.Fatal, EnumConverter.ToEnum<LogLevel>(LogLevel.Fatal));
            Assert.Equal(LogLevel.Fatal, EnumConverter.ToEnum<LogLevel>("Fatal"));
        }
    }
}
