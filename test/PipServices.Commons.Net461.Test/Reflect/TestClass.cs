using System;

namespace PipServices.Commons.Test.Reflect
{
    public class TestClass
    {
        private float _privateField = 123;
        public string PublicField = "ABC";

        public TestClass() { }

        public TestClass(int arg1) { }

        protected long GetPrivateProp() { return 543; }
        protected void SetPrivateProp(long value) { }

        public DateTimeOffset PublicProp { get; set; } = DateTimeOffset.UtcNow;

        private void PrivateMethod() { }

        public int PublicMethod(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
    }
}
