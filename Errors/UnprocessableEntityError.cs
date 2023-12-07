using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Errors;

public class UnprocessableEntityError : Error
{
    public UnprocessableEntityError(string message)
        : base(message)
    {
    }
}
