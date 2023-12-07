using System.Linq;
using System.Net;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;

namespace TNRD.Zeepkist.GTR.SDK.Extensions;

public static class ResultExtensions
{
    public static bool IsNotFound(this ResultBase resultBase)
    {
        if (resultBase.IsSuccess)
            return false;

        StatusCodeReason statusCodeReason =
            (StatusCodeReason)resultBase.Reasons.FirstOrDefault(x => x is StatusCodeReason);

        if (statusCodeReason == null)
            return false;

        return statusCodeReason.StatusCode == HttpStatusCode.NotFound;
    }
}
