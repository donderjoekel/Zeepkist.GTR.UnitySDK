namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class AuthClient : ClientBase
{
    public static AuthClient Create(Sdk sdk, string baseAddress)
    {
        return new AuthClient(sdk, baseAddress);
    }

    /// <inheritdoc />
    private AuthClient(Sdk sdk, string baseAddress)
        : base(sdk, baseAddress)
    {
    }
}
