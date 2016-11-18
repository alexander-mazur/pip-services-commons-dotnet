//using Xunit;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using PipServices.Commons.Reflect;

//namespace PipServices.Commons.Test.Reflect
//{
//    public class PropertyReflectorTest
//    {
//        [Fact]
//        public void TestGetProperty()
//        {
//            TestClass obj = new TestClass();

//            object value = PropertyReflector.GetProperty(obj, "privateField");
//            Assert.IsNull(value);

//            value = PropertyReflector.GetProperty(obj, "intField");
//            Assert.AreEqual(12345, value);

//            value = PropertyReflector.GetProperty(obj, "StringProperty");
//            Assert.IsNotNull(value);
//        }

//        [Fact]
//        public void TestGetProperties()
//        {
//            TestClass obj = new TestClass();
//            List<string> names = PropertyReflector.GetPropertyNames(obj);
//            Assert.AreEqual(8, names.Count);
//            Assert.IsTrue(names.Contains("intField"));
//            Assert.IsTrue(names.Contains("StringProperty"));
//            Assert.IsFalse(names.Contains("privateField"));
//            Assert.IsFalse(names.Contains("PrivateProperty"));

//            Dictionary<string, object> map = PropertyReflector.GetProperties(obj);
//            Assert.IsTrue(map.Count > 2);
//            Assert.AreEqual(12345, map["intField"]);
//            Assert.AreEqual("ABC", map["StringProperty"]);
//        }

//        [Fact]
//        public void TestSetProperties()
//        {
//            TestClass obj = new TestClass();

//            Dictionary<string, object> map = new Dictionary<string, object>()
//            {
//                { "intField", 321 },
//                { "StringProperty", "XYZ" },
//                { "UnknownProperty", 111 }
//            };
//            PropertyReflector.SetProperties(obj, map);

//            Assert.AreEqual(321, obj.intField);
//            Assert.AreEqual("XYZ", obj.StringProperty);
//        }
//    }
//}
