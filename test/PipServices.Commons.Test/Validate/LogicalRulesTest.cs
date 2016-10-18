//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//    [TestClass]
//    public class LogicalRulesTest
//    {
//        [TestMethod]
//        public void TestOrRule()
//        {
//            Schema schema = new Schema().WithRule(
//                new OrRule(
//                    new ValueComparisonRule("=", 1),
//                    new ValueComparisonRule("=", 2)
//                )
//            );
//            List<ValidationResult> result = schema.Validate(-100);
//            Assert.AreEqual(2, result.Count);

//            result = schema.Validate(1);
//            Assert.AreEqual(0, result.Count);

//            result = schema.Validate(200);
//            Assert.AreEqual(2, result.Count);
//        }

//        [TestMethod]
//        public void TestAndRule()
//        {
//            Schema schema = new Schema().WithRule(
//                new AndRule(
//                    new ValueComparisonRule(">", 0),
//                    new ValueComparisonRule("<", 200)
//                )
//            );
//            List<ValidationResult> result = schema.Validate(-100);
//            Assert.AreEqual(1, result.Count);

//            result = schema.Validate(100);
//            Assert.AreEqual(0, result.Count);

//            result = schema.Validate(200);
//            Assert.AreEqual(1, result.Count);
//        }
//    }
//}
