using System;
using System.Linq;
using System.Reflection;

namespace PipServices.Commons.Random
{
    public class RandomEnum
    {
        public static T NextEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            Type etype = typeof(T);
            T[] vals = etype.GetEnumValues().Cast<T>().ToArray();
            return RandomArray.Pick(vals);
        }
    }
}
