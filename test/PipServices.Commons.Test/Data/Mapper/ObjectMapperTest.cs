using System.Collections.Generic;
using PipServices.Commons.Data;
using PipServices.Commons.Data.Mapper;
using PipServices.Commons.Log;
using Xunit;


namespace PipServices.Commons.Test.Data.Mapper
{
    public sealed class ObjectMapperTest
    {
        internal sealed class PlainClassA
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
        }

        internal sealed class PlainClassB
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public long Property3 { get; set; }
        }

        internal sealed class ClassA
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public IEnumerable<object> Property3 { get; set; }
            public PlainClassA Property4 { get; set; }

            public ClassA()
            {
                //Target collection must be created 
                Property3 = new List<object>();
            }
        }

        internal sealed class ClassB
        {
            public int Property1 { get; set; }
            public string Property2 { get; set; }
            public IEnumerable<object> Property3 { get; set; }
            public PlainClassA Property4 { get; set; }
            public long Property5 { get; set; }

            public ClassB()
            {
                //Target collection must be created 
                Property3 = new List<object>();
            }
        }

        [Fact]
        public void MapTo_MapsPlainObjectToWider()
        {
            var objectA = new ClassA()
            {
                Property1 = 100,
                Property2 = "Property2",
                Property3 = new object[] {"1", 2, "3"}
            };

            var objectB = ObjectMapper.MapTo<ClassB>(objectA);

            Assert.NotSame(objectA, objectB);
            Assert.IsType<ClassB>(objectB);
            Assert.Equal(objectA.Property1, objectB.Property1);
            Assert.Equal(objectA.Property2, objectB.Property2);
            Assert.Equal(objectA.Property3, objectB.Property3);

            Assert.NotSame(objectA.Property4, objectB.Property4);

            Assert.Equal(0, objectB.Property5);
        }

        [Fact]
        public void MapTo_MapsPlainObjectToNarrower()
        {
            var objectB = new ClassB()
            {
                Property1 = 100,
                Property2 = "Property2",
                Property3 = new object[] { "1", 2, "3" },
                Property5 = 10
            };

            var objectA = ObjectMapper.MapTo<ClassA>(objectB);

            Assert.NotSame(objectB, objectA);
            Assert.IsType<ClassA>(objectA);
            Assert.Equal(objectA.Property1, objectB.Property1);
            Assert.Equal(objectA.Property2, objectB.Property2);
            Assert.Equal(objectA.Property3, objectB.Property3);
        }
    }
}
