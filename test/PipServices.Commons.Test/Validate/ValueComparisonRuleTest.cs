//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace PipServices.Commons.Test.Validate
//{
//[TestClass]
//public class ValueComparisonRuleTest
//{
//    [TestMethod]
//    public void TestEqualComparison()
//    {
//        Schema schema = new Schema().WithRule(new ValueComparisonRule("EQ", 123));
//        List<ValidationResult> results = schema.Validate(123);
//        Assert.AreEqual(0, results.Count);

//        results = schema.Validate(432);
//        Assert.AreEqual(1, results.Count);

//        schema = new Schema().WithRule(new ValueComparisonRule("EQ", "ABC"));
//        results = schema.Validate("ABC");
//        Assert.AreEqual(0, results.Count);
//    }

//    [TestMethod]
//    public void TestNotEqualComparison()
//    {
//        Schema schema = new Schema().WithRule(new ValueComparisonRule("NE", 123));
//        List<ValidationResult> results = schema.Validate(123);
//        Assert.AreEqual(1, results.Count);

//        results = schema.Validate(432);
//        Assert.AreEqual(0, results.Count);

//        schema = new Schema().WithRule(new ValueComparisonRule("NE", "ABC"));
//        results = schema.Validate("XYZ");
//        Assert.AreEqual(0, results.Count);
//    }

//    [TestMethod]
//    public void TestLessComparison()
//    {
//        Schema schema = new Schema().WithRule(new ValueComparisonRule("LE", 123));
//        List<ValidationResult> results = schema.Validate(123);
//        Assert.AreEqual(0, results.Count);

//        results = schema.Validate(432);
//        Assert.AreEqual(1, results.Count);

//        schema = new Schema().WithRule(new ValueComparisonRule("LT", 123));
//        results = schema.Validate(123);
//        Assert.AreEqual(1, results.Count);
//    }

//    [TestMethod]
//    public void TestMoreComparison()
//    {
//        Schema schema = new Schema().WithRule(new ValueComparisonRule("GE", 123));
//        List<ValidationResult> results = schema.Validate(123);
//        Assert.AreEqual(0, results.Count);

//        results = schema.Validate(432);
//        Assert.AreEqual(0, results.Count);

//        schema = new Schema().WithRule(new ValueComparisonRule("GT", 123));
//        results = schema.Validate(123);
//        Assert.AreEqual(1, results.Count);
//    }

//    [TestMethod]
//    public void TestMatchComparison()
//    {
//        Schema schema = new Schema().WithRule(new ValueComparisonRule("LIKE", "A.*"));
//        List<ValidationResult> results = schema.Validate("ABC");
//        Assert.AreEqual(0, results.Count);

//        results = schema.Validate("XYZ");
//        Assert.AreEqual(1, results.Count);
//    }
//}
//}
