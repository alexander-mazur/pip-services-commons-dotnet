using System.Collections;
using System.Linq;
using System.Reflection;

namespace PipServices.Commons.Data.Mapper
{
    internal sealed class ObjectMapperStrategy : IObjectMapperStrategy
    {
        private bool IsPrimitiveType(object obj)
        {
            var type = obj.GetType();

            return type == typeof(string) || type == typeof(int) || type == typeof(long) || type == typeof(decimal) ||
                   type == typeof(char) || type == typeof(decimal) || type == typeof(bool) || type == typeof(byte) ||
                   type == typeof(double) || type == typeof(float) || type == typeof(sbyte) || type == typeof(short) ||
                   type == typeof(uint) || type == typeof(ulong) || type == typeof(ushort);
        }

        public void Transfer<TS, TT>(IObjectMapper mapper, TS objectSource, TT objectTarget,
            PropertyInfo propertyInfoSource, PropertyInfo propertyInfoTarget)
            where TS : class
            where TT : class
        {
            var propertyValueSource = propertyInfoSource.GetValue(objectSource);
            var propertyValueTarget = propertyInfoTarget.GetValue(objectTarget);

            if (propertyValueSource == null)
                return;

            var valueSourceType = propertyValueSource.GetType();
            var valueSourceTypeInfo = valueSourceType.GetTypeInfo();

            var valueTargetType = propertyInfoTarget.PropertyType;
            var valueTargetTypeInfo = valueTargetType.GetTypeInfo();

            if (valueSourceTypeInfo.IsClass && valueSourceType != typeof(string) && !valueSourceTypeInfo.IsArray &&
                !valueSourceTypeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
            {
                var methodInfo = mapper.GetType().GetTypeInfo().GetMethod(nameof(mapper.Transfer));
                var genericMethodInfo = methodInfo.MakeGenericMethod(valueSourceType, valueTargetType);
                var result = genericMethodInfo.Invoke(mapper, new[] { propertyValueSource });

                propertyInfoTarget.SetValue(objectTarget, result);

                return;
            }

            if (valueSourceType != typeof(string) &&
                (valueSourceTypeInfo.IsArray || valueSourceTypeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable))))
            {
                var source = (IEnumerable)propertyValueSource;

                object firstEntry = null;
                foreach (var item in source)
                {
                    firstEntry = item;
                    break;
                }

                var entrySourceType = firstEntry?.GetType();

                if (entrySourceType != null)
                {
                    var entrySourceTypeInfo = entrySourceType.GetTypeInfo();
                    var entryTargetType = propertyValueTarget.GetType().GetTypeInfo().GetGenericArguments()[0];

                    if (entrySourceTypeInfo.IsClass)
                    {
                        var methodParameters = new[]
                        {
                            entryTargetType
                        };

                        var method = propertyValueTarget.GetType().GetRuntimeMethod("Add", methodParameters);

                        foreach (var entrySource in source)
                        {
                            if (entrySource == null || method == null)
                                continue;

                            if (IsPrimitiveType(entrySource))
                            {
                                var parameters = new[]
                                {
                                    entrySource
                                };

                                method.Invoke(propertyValueTarget, parameters);
                            }
                            else
                            {
                                var methodInfo = mapper.GetType().GetTypeInfo().GetMethod(nameof(mapper.Transfer));
                                var genericMethodInfo = methodInfo.MakeGenericMethod(entrySourceType, entryTargetType);
                                var entryTarget = genericMethodInfo.Invoke(mapper, new[] { entrySource });

                                var parameters = new[]
                                {
                                    entryTarget
                                };

                                method.Invoke(propertyValueTarget, parameters);
                            }
                        }
                    }
                    else
                    {
                        var methodParameters = new[]
                        {
                            entrySourceType
                        };

                        var method = propertyValueTarget.GetType().GetRuntimeMethod("Add", methodParameters);

                        foreach (var entrySource in source)
                        {
                            var parameters = new[]
                            {
                                entrySource
                            };

                            method.Invoke(propertyValueTarget, parameters);
                        }
                    }

                    return;
                }
            }

            propertyInfoTarget.SetValue(objectTarget, propertyValueSource);
        }
    }
}