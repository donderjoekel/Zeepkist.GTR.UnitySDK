using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.SDK.Client;
using UnityEngine.LowLevel;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class SdkInitializer
{
    /// <summary>
    /// Initializes the SDK with the default backend address
    /// </summary>
    public static void Initialize()
    {
        Initialize("https://api.zeepkist-gtr.com", "https://auth.zeepkist-gtr.com");
    }

    /// <summary>
    /// Initializes the SDK with a custom backend address
    /// </summary>
    /// <param name="apiAddress">The base address to use for all api requests</param>
    /// <param name="authAddress">The base address to use for all auth requests</param>
    public static void Initialize(string apiAddress, string authAddress)
    {
        // Create the actual ApiClient
        ApiClient.Create(apiAddress);
        AuthClient.Create(authAddress);

        // Initialize the player loop helper, this is to reduce issues with UniTask
        if (PlayerLoopHelper.IsInjectedUniTaskPlayerLoop())
            return;

        PlayerLoopSystem loop = PlayerLoop.GetCurrentPlayerLoop();
        PlayerLoopHelper.Initialize(ref loop);
    }
}
