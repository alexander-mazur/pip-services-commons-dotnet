//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//    [TestClass]
//    public class InclusionRulesTest
//    {
//        [TestMethod]
//        public void TestIncludedRule()
//        {
//            Schema schema = new Schema().WithRule(new IncludedRule("AAA", "BBB", "CCC", null));
//            List<ValidationResult> results = schema.Validate("AAA");
//            Assert.AreEqual(0, results.Count);

//            results = schema.Validate("ABC");
//            Assert.AreEqual(1, results.Count);
//        }

//        [TestMethod]
//        public void TestExcludedRule()
//        {
//            Schema schema = new Schema().WithRule(new ExcludedRule("AAA", "BBB", "CCC", null));
//            List<ValidationResult> results = schema.Validate("AAA");
//            Assert.AreEqual(1, results.Count);

//            results = schema.Validate("ABC");
//            Assert.AreEqual(0, results.Count);
//        }
//    }
//}
