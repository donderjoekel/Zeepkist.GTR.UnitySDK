using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public class RecordsApi : IRecordsApi
{
    private readonly Sdk sdk;

    public RecordsApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<RecordsGetResponseDTO>> Get(Action<RecordsGetRequestDTOBuilder> builder)
    {
        RecordsGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<RecordsGetResponseDTO>($"records?{dto.ToQueryString()}");
    }

    public UniTask<Result<RecordsGetRecentResponseDTO>> GetRecent(
        Action<RecordsGetRecentRequestDTOBuilder> builder
    )
    {
        RecordsGetRecentRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRecentRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<RecordsGetRecentResponseDTO>($"records/recent?{dto.ToQueryString()}");
    }
}
