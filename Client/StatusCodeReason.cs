using System.Collections.Generic;
using System.Net;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class StatusCodeReason : IReason
{
    /// <inheritdoc />
    public string Message { get; }

    /// <inheritdoc />
    public Dictionary<string, object> Metadata { get; }

    public HttpStatusCode StatusCode { get; }

    public StatusCodeReason(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}
