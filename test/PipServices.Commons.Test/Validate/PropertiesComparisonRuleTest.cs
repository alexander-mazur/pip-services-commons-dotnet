//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//[TestClass]
//public class PropertiesComparisonRuleTest
//{
//    [TestMethod]
//    public void TestPropertiesComparison()
//    {
//        TestObject obj = new TestObject();
//        Schema schema = new Schema().WithRule(new PropertiesComparisonRule("StringProperty", "EQ", "NullProperty"));

//        obj.StringProperty = "ABC";
//        obj.NullProperty = "ABC";
//        List<ValidationResult> results = schema.Validate(obj);
//        Assert.AreEqual(0, results.Count);

//        obj.NullProperty = "XYZ";
//        results = schema.Validate(obj);
//        Assert.AreEqual(1, results.Count);
//    }
//}
//}
