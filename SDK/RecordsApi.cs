using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class RecordsApi
{
    public static UniTask<Result<RecordsGetResponseDTO>> Get(Action<RecordsGetRequestDTOBuilder> builder)
    {
        RecordsGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Get<RecordsGetResponseDTO>($"records?{dto.ToQueryString()}");
    }

    public static UniTask<Result<RecordsGetRecentResponseDTO>> GetRecent(
        Action<RecordsGetRecentRequestDTOBuilder> builder
    )
    {
        RecordsGetRecentRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRecentRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Get<RecordsGetRecentResponseDTO>($"records/recent?{dto.ToQueryString()}");
    }
}
