using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

internal class VersionApi : IVersionApi
{
    private readonly Sdk sdk;

    public VersionApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<VersionInfo>> GetVersionInfo()
    {
        return sdk.ApiClient.Get<VersionInfo>("version", false, false);
    }
}
