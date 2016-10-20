using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Reflect
{
    public sealed class RecursiveObjectReader
    {
        //private static boolean performHasProperty(Object obj, String[] names, int nameIndex)
        //{
        //    if (nameIndex < names.length - 1)
        //    {
        //        Object value = ObjectReader.getProperty(obj, names[nameIndex]);
        //        if (value != null)
        //            return performHasProperty(value, names, nameIndex + 1);
        //        else
        //            return false;
        //    }
        //    else
        //        return ObjectReader.hasProperty(obj, names[nameIndex]);
        //}

        //public static boolean hasProperty(Object obj, String name)
        //{
        //    if (obj == null || name == null) return false;

        //    String[] names = name.split("\\.");
        //    if (names == null || names.length == 0)
        //        return false;

        //    return performHasProperty(obj, names, 0);
        //}

        //private static Object performGetProperty(Object obj, String[] names, int nameIndex)
        //{
        //    if (nameIndex < names.length - 1)
        //    {
        //        Object value = ObjectReader.getProperty(obj, names[nameIndex]);
        //        if (value != null)
        //            return performGetProperty(value, names, nameIndex + 1);
        //        else
        //            return null;
        //    }
        //    else
        //        return ObjectReader.getProperty(obj, names[nameIndex]);
        //}

        //public static Object getProperty(Object obj, String name)
        //{
        //    if (obj == null || name == null) return null;

        //    String[] names = name.split("\\.");
        //    if (names == null || names.length == 0)
        //        return null;

        //    return performGetProperty(obj, names, 0);
        //}

        //private static boolean isSimpleValue(Object value)
        //{
        //    TypeCode code = TypeConverter.toTypeCode(value);
        //    return code != TypeCode.Array && code != TypeCode.Map && code != TypeCode.Object;
        //}

        //private static void performGetPropertyNames(Object obj, String path,
        //    List<String> result, List<Object> cycleDetect)
        //{

        //    Map<String, Object> map = ObjectReader.getProperties(obj);

        //    if (map.size() != 0 && cycleDetect.size() < 100)
        //    {
        //        cycleDetect.add(obj);
        //        try
        //        {
        //            for (Map.Entry<String, Object> entry : map.entrySet())
        //            {
        //                Object value = entry.getValue();

        //                // Prevent cycles 
        //                if (cycleDetect.contains(value))
        //                    continue;

        //                String key = path != null ? path + "." + entry.getKey() : entry.getKey();

        //                // Add simple values directly
        //                if (isSimpleValue(value))
        //                    result.add(key);
        //                // Recursively go to elements
        //                else
        //                    performGetPropertyNames(value, key, result, cycleDetect);
        //            }
        //        }
        //        finally
        //        {
        //            cycleDetect.remove(obj);
        //        }
        //    }
        //    else
        //    {
        //        if (path != null)
        //            result.add(path);
        //    }
        //}

        //public static List<String> getPropertyNames(Object obj)
        //{
        //    List<String> propertyNames = new ArrayList<String>();

        //    if (obj == null)
        //    {
        //        return propertyNames;
        //    }
        //    else
        //    {
        //        List<Object> cycleDetect = new ArrayList<Object>();
        //        performGetPropertyNames(obj, null, propertyNames, cycleDetect);
        //        return propertyNames;
        //    }
        //}

        //private static void performGetProperties(Object obj, String path,
        //    Map<String, Object> result, List<Object> cycleDetect)
        //{

        //    Map<String, Object> map = ObjectReader.getProperties(obj);

        //    if (map.size() != 0 && cycleDetect.size() < 100)
        //    {
        //        cycleDetect.add(obj);
        //        try
        //        {
        //            for (Map.Entry<String, Object> entry : map.entrySet())
        //            {
        //                Object value = entry.getValue();

        //                // Prevent cycles 
        //                if (cycleDetect.contains(value))
        //                    continue;

        //                String key = path != null ? path + "." + entry.getKey() : entry.getKey();

        //                // Add simple values directly
        //                if (isSimpleValue(value))
        //                    result.put(key, value);
        //                // Recursively go to elements
        //                else
        //                    performGetProperties(value, key, result, cycleDetect);
        //            }
        //        }
        //        finally
        //        {
        //            cycleDetect.remove(obj);
        //        }
        //    }
        //    else
        //    {
        //        if (path != null)
        //            result.put(path, obj);
        //    }
        //}

        //public static Map<String, Object> getProperties(Object obj)
        //{
        //    Map<String, Object> properties = new HashMap<String, Object>();

        //    if (obj == null)
        //    {
        //        return properties;
        //    }
        //    else
        //    {
        //        List<Object> cycleDetect = new ArrayList<Object>();
        //        performGetProperties(obj, null, properties, cycleDetect);
        //        return properties;
        //    }
        //}
    }
}
