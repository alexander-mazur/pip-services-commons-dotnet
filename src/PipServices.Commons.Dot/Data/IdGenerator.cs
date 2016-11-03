using System;

namespace PipServices.Commons.Data
{
    public class IdGenerator
    {
        private static Random _rand = new Random();

        public static string NextShort()
        {
            return Math.Floor((decimal)100000000 + _rand.Next() * 899999999).ToString();
        }

        public static string NextLong()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}