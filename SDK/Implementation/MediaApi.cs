using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

internal class MediaApi : IMediaApi
{
    private readonly Sdk sdk;

    public MediaApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<MediaGetByRecordResponseDTO>> GetByRecord(int id)
    {
        return sdk.ApiClient.Get<MediaGetByRecordResponseDTO>("media/record/" + id, false);
    }
}
