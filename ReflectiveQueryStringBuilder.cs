using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Steamworks.Ugc;

namespace TNRD.Zeepkist.GTR.SDK;

internal static class ReflectiveQueryStringBuilder
{
    public static string BuildQuery(object obj)
    {
        List<string> queries = new();
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (PropertyInfo property in properties)
        {
            if (!property.CanRead)
                continue;

            object value = property.GetValue(obj);

            switch (value)
            {
                case null:
                    continue;
                case Array array:
                    queries.AddRange(array.Cast<object>().Select(v => $"{property.Name}={v}"));
                    break;
                default:
                    queries.Add($"{property.Name}={value}");
                    break;
            }
        }

        return string.Join("&", queries);
    }

    public static string ToQueryString(this object obj)
    {
        return BuildQuery(obj);
    }
}
