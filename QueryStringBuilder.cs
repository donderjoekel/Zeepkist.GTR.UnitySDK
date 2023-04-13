using System.Collections.Generic;

namespace TNRD.Zeepkist.GTR.SDK;

internal class QueryStringBuilder
{
    public static QueryStringBuilder Create()
    {
        return new QueryStringBuilder();
    }

    private readonly List<string> queries = new List<string>();

    private QueryStringBuilder()
    {
        
    }

    public QueryStringBuilder Add(string key, object value)
    {
        queries.Add($"{key}={value.ToString()}");
        return this;
    }

    public QueryStringBuilder AddOptional(string key, object value, object defaultValue = null)
    {
        if (value == null && defaultValue == null)
            return this;

        if (value != null)
            queries.Add($"{key}={value.ToString()}");
        else
            queries.Add($"{key}={defaultValue.ToString()}");

        return this;
    }

    public string Build()
    {
        return string.Join("&", queries);
    }
}
