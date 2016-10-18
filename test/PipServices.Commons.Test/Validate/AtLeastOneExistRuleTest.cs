//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//    [TestClass]
//    public class AtLeastOneExistRuleTest
//    {
//        [TestMethod]
//        public void TestOnlyOneExistRule()
//        {
//            TestObject obj = new TestObject();
//            Schema schema = new Schema().WithRule(new AtLeastOneExistRule("MissingProperty", "StringProperty", "NullProperty"));
//            List<ValidationResult> results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);

//            schema = new Schema().WithRule(new AtLeastOneExistRule("StringProperty", "NullProperty", "intField"));
//            results = schema.Validate(obj);
//            Assert.AreEqual(0, results.Count);

//            schema = new Schema().WithRule(new AtLeastOneExistRule("MissingProperty", "NullProperty"));
//            results = schema.Validate(obj);
//            Assert.AreEqual(1, results.Count);
//        }
//    }
//}
