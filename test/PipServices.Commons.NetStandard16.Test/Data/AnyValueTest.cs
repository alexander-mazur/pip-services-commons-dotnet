﻿using System;
using PipServices.Commons.Data;
using PipServices.Commons.Log;
using Xunit;

namespace PipServices.Commons.Test.Data
{
    public sealed class AnyValueTest
    {
        [Fact]
        public void TestGetAndSetAnyValue()
        {
            var value = new AnyValue();
            Assert.Null(value.GetAsObject());

            value.SetAsObject(1);
            Assert.Equal(1, value.GetAsInteger());
            Assert.Equal(1.0, value.GetAsFloat());
            Assert.Equal("1", value.GetAsString());
            Assert.Equal(TimeSpan.FromMilliseconds(1), value.GetAsTimeSpan());
            Assert.Equal(LogLevel.Fatal, value.GetAsEnum<LogLevel>());
        }

        [Fact]
        public void TestEqualAnyValue()
        {
            var value = new AnyValue(1);

            Assert.True(value.Equals(1));
            Assert.True(value.Equals(1.0));
            Assert.True(value.Equals("1"));
            Assert.True(value.Equals(TimeSpan.FromMilliseconds(1)));
            Assert.True(value.EqualsAs<LogLevel>(LogLevel.Fatal));
        }
    }
}
