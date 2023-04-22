namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class ApiClient : ClientBase
{
    public static ApiClient Instance { get; private set; }

    public static void Create(string baseAddress, bool logRequestUrl, bool logResponseOutput)
    {
        Instance = new ApiClient(baseAddress);
        Instance.LogRequestUrl = logRequestUrl;
        Instance.LogResponseOutput = logResponseOutput;
    }

    /// <inheritdoc />
    private ApiClient(string baseAddress)
        : base(baseAddress)
    {
    }
}
