﻿using System.Collections.Generic;

namespace PipServices.Commons.Test.Reflect
{
    public class TestObject
    {
        public TestObject() { }

        private float privateField = 124;
        private string PrivateProperty { get; set; } = "XYZ";

        public int intField = 12345;
        public string StringProperty { get; set; } = "ABC";
        public object NullProperty { get; set; } = null;
        public int[] IntArrayProperty { get; set; } = new int[] { 1, 2, 3 };
        public List<string> StringListProperty { get; set; } = new List<string>(new string[] { "AAA", "BBB" });
        public Dictionary<string, int> DictProperty = new Dictionary<string, int>() { { "Key1", 111 }, { "Key2", 222 } };
    }
}