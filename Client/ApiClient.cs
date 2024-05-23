namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class ApiClient : ClientBase
{
    public static ApiClient Create(Sdk sdk, string baseAddress, bool logRequestUrl, bool logResponseOutput)
    {
        ApiClient instance = new(sdk, baseAddress);
        instance.LogRequestUrl = logRequestUrl;
        instance.LogResponseOutput = logResponseOutput;
        return instance;
    }

    /// <inheritdoc />
    private ApiClient(Sdk sdk, string baseAddress)
        : base(sdk, baseAddress)
    {
    }
}
