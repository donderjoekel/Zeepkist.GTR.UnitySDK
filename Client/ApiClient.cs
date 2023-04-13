namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class ApiClient : ClientBase
{
    public static ApiClient Instance { get; private set; }

    public static void Create(string baseAddress)
    {
        Instance = new ApiClient(baseAddress);
    }

    /// <inheritdoc />
    private ApiClient(string baseAddress)
        : base(baseAddress)
    {
    }
}
