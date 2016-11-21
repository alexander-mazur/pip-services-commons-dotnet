using System;

namespace PipServices.Commons.Data
{
    public class IdGenerator
    {
        private static readonly System.Random _random = new System.Random();

        public static string NextShort()
        {
            return Math.Floor((decimal)100000000 + _random.Next() * 899999999).ToString();
        }

        public static string NextLong()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}