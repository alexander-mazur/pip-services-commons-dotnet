using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace PipServices.Commons.Reflect
{
    // Todo: Add HasProperty
    // Todo: Process public fields

    public class PropertyReflector
    {
        public static object GetProperty(string obj, string name)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if(name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var typeInfo = obj.GetType().GetTypeInfo();
            return typeInfo.GetProperty(name).GetValue(obj);
        }

        public static void SetProperty(object obj, string name, object value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            var typeInfo = obj.GetType().GetTypeInfo();
            typeInfo.GetProperty(name).SetValue(obj, value);
        }

        public static void SetProperties(object obj, Dictionary<string, object> values)
        {
            if (values == null || values.Count == 0) return;
            foreach (var key in values.Keys)
            {
                SetProperty(obj, key, values[key]);
            }
        }
    }
}