using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.SDK.Client;
using UnityEngine.LowLevel;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class SdkInitializer
{
    public const string DEFAULT_API_ADDRESS = "https://api.zeepkist-gtr.com";
    public const string DEFAULT_AUTH_ADDRESS = "https://auth.zeepkist-gtr.com";

    /// <summary>
    /// Initializes the SDK with the default backend address and no logging
    /// </summary>
    public static void Initialize()
    {
        Initialize(DEFAULT_API_ADDRESS, DEFAULT_AUTH_ADDRESS, false, false);
    }

    /// <summary>
    /// Initializes the SDK with the default backend address
    /// </summary>
    /// <param name="logRequestUrl">Setting this to true will log the URL of each request</param>
    /// <param name="logResponseOutput">Setting this to true will log the response text of each request</param>
    public static void Initialize(bool logRequestUrl, bool logResponseOutput)
    {
        Initialize(DEFAULT_API_ADDRESS, DEFAULT_AUTH_ADDRESS, logRequestUrl, logResponseOutput);
    }

    /// <summary>
    /// Initializes the SDK with a custom backend address
    /// </summary>
    /// <param name="apiAddress">The base address to use for all api requests</param>
    /// <param name="authAddress">The base address to use for all auth requests</param>
    /// <param name="logRequestUrl">Setting this to true will log the URL of each request</param>
    /// <param name="logResponseOutput">Setting this to true will log the response text of each request</param>
    public static void Initialize(string apiAddress, string authAddress, bool logRequestUrl, bool logResponseOutput)
    {
        // Create the actual ApiClient
        ApiClient.Create(apiAddress, logRequestUrl, logResponseOutput);
        AuthClient.Create(authAddress);

        // Initialize the player loop helper, this is to reduce issues with UniTask
        if (PlayerLoopHelper.IsInjectedUniTaskPlayerLoop())
            return;

        PlayerLoopSystem loop = PlayerLoop.GetCurrentPlayerLoop();
        PlayerLoopHelper.Initialize(ref loop);
    }
}
