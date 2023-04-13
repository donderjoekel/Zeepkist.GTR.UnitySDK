namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class AuthClient : ClientBase
{
    public static AuthClient Instance { get; private set; }

    public static void Create(string baseAddress)
    {
        Instance = new AuthClient(baseAddress);
    }

    /// <inheritdoc />
    private AuthClient(string baseAddress)
        : base(baseAddress)
    {
    }
}
