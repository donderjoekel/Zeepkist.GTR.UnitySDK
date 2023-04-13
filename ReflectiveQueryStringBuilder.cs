using System;
using System.Collections.Generic;
using System.Reflection;

namespace TNRD.Zeepkist.GTR.SDK;

internal static class ReflectiveQueryStringBuilder
{
    public static string BuildQuery(object obj)
    {
        List<string> queries = new List<string>();
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        foreach (PropertyInfo property in properties)
        {
            if (!property.CanRead)
                continue;
            object value = property.GetValue(obj);
            if (value == null)
                continue;
            queries.Add($"{property.Name}={value}");
        }

        return string.Join("&", queries);
    }

    public static string ToQueryString(this object obj)
    {
        return BuildQuery(obj);
    }
}
