﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//    [TestClass]
//    public class SchemaTest
//    {
//        [TestMethod]
//        public void TestEmptySchema()
//        {
//            var schema = new ObjectSchema();
//            var results = schema.Validate(null);
//            Assert.AreEqual(0, results.Count);
//        }

//        [TestMethod]
//        public void TestRequired()
//        {
//            var schema = new Schema().MakeRequired();
//            var results = schema.Validate(null);
//            Assert.AreEqual(1, results.Count);
//        }

//        [TestMethod]
//        public void TestUnexpected()
//        {
//            var schema = new ObjectSchema();
//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(8, results.Count);
//        }

//        [TestMethod]
//        public void TestOptionalProperties()
//        {
//            var schema = new ObjectSchema()
//                .WithOptionalProperty("intField", null)
//                .WithOptionalProperty("StringProperty", null)
//                .WithOptionalProperty("NullProperty", null)
//                .WithOptionalProperty("IntArrayProperty", null)
//                .WithOptionalProperty("StringListProperty", null)
//                .WithOptionalProperty("DictProperty", null)
//                .WithOptionalProperty("SubObjectProperty", null)
//                .WithOptionalProperty("SubArrayProperty", null);

//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);
//        }

//        [TestMethod]
//        public void TestRequiredProperties()
//        {
//            var schema = new ObjectSchema()
//                .WithRequiredProperty("intField", null)
//                .WithRequiredProperty("StringProperty", null)
//                .WithRequiredProperty("NullProperty", null)
//                .WithRequiredProperty("IntArrayProperty", null)
//                .WithRequiredProperty("StringListProperty", null)
//                .WithRequiredProperty("DictProperty", null)
//                .WithRequiredProperty("SubObjectProperty", null)
//                .WithRequiredProperty("SubArrayProperty", null);

//            var obj = new TestObject();
//            obj.SubArrayProperty = null;

//            var results = schema.Validate(obj);
//            Assert.AreEqual(2, results.Count);
//        }

//        [TestMethod]
//        public void TestObjectTypes()
//        {
//            var schema = new ObjectSchema()
//                .WithRequiredProperty("intField", typeof(int))
//                .WithRequiredProperty("StringProperty", typeof(string))
//                .WithOptionalProperty("NullProperty", typeof(object))
//                .WithRequiredProperty("IntArrayProperty", typeof(int[]))
//                .WithRequiredProperty("StringListProperty", typeof(List<string>))
//                .WithRequiredProperty("DictProperty", typeof(Dictionary<string, int>))
//                .WithRequiredProperty("SubObjectProperty", typeof(TestSubObject))
//                .WithRequiredProperty("SubArrayProperty", typeof(TestSubObject[]));

//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);
//        }

//        [TestMethod]
//        public void TestStringTypes()
//        {
//            var schema = new ObjectSchema()
//                .WithRequiredProperty("intField", "Int32")
//                .WithRequiredProperty("StringProperty", "String")
//                .WithOptionalProperty("NullProperty", "Object")
//                .WithRequiredProperty("IntArrayProperty", "Int32[]")
//                .WithRequiredProperty("StringListProperty", "List`1")
//                .WithRequiredProperty("DictProperty", "Dictionary`2")
//                .WithRequiredProperty("SubObjectProperty", "TestSubObject")
//                .WithRequiredProperty("SubArrayProperty", "TestSubObject[]");

//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);
//        }

//        [TestMethod]
//        public void TestSubSchema()
//        {
//            var subSchema = new ObjectSchema()
//                .WithRequiredProperty("Id", "String")
//                .WithRequiredProperty("FLOATFIELD", "Single")
//                .WithOptionalProperty("nullproperty", "Object");

//            var schema = new ObjectSchema()
//                .WithRequiredProperty("intField", "Int32")
//                .WithRequiredProperty("StringProperty", "String")
//                .WithOptionalProperty("NullProperty", "Object")
//                .WithRequiredProperty("IntArrayProperty", "Int32[]")
//                .WithRequiredProperty("StringListProperty", "List`1")
//                .WithRequiredProperty("DictProperty", "Dictionary`2")
//                .WithRequiredProperty("SubObjectProperty", subSchema)
//                .WithRequiredProperty("SubArrayProperty", "TestSubObject[]");

//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);
//        }

//        [TestMethod]
//        public void TestArrayAndMapSchema()
//        {
//            var subSchema = new ObjectSchema()
//                .WithRequiredProperty("Id", "String")
//                .WithRequiredProperty("FLOATFIELD", "Single")
//                .WithOptionalProperty("nullproperty", "Object");

//            var schema = new ObjectSchema()
//                .WithRequiredProperty("intField", "Int32")
//                .WithRequiredProperty("StringProperty", "String")
//                .WithOptionalProperty("NullProperty", "Object")
//                .WithRequiredProperty("IntArrayProperty", new ArraySchema("Int32"))
//                .WithRequiredProperty("StringListProperty", new ArraySchema("String"))
//                .WithRequiredProperty("DictProperty", new MapSchema("String", "Int32"))
//                .WithRequiredProperty("SubObjectProperty", subSchema)
//                .WithRequiredProperty("SubArrayProperty", new ArraySchema(subSchema));

//            var obj = new TestObject();
//            var results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);
//        }

//    }
//}
