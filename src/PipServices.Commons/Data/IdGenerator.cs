using System;

namespace PipServices.Commons.Data
{
    public class IdGenerator
    {
        public static string nextShort()
        {
            return Math.Floor((decimal)100000000 + new Random().Next() * 899999999).ToString();
        }

        public static String nextLong()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}