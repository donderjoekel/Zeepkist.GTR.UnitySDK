using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

internal class LevelApi : ILevelApi
{
    private readonly Sdk sdk;

    public LevelApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<LevelsGetPointsByLevelResponseDTO>> GetPointsByLevel(string level)
    {
        return sdk.ApiClient.Get<LevelsGetPointsByLevelResponseDTO>($"levels/points/{level}", false, true, true);
    }
}
