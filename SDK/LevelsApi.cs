using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class LevelsApi
{
    public static UniTask<Result<LevelsGetResponseDTO>> Get(Action<LevelsGetRequestDTOBuilder> builder)
    {
        LevelsGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        LevelsGetRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Get<LevelsGetResponseDTO>($"levels?{dto.ToQueryString()}");
    }

    public static UniTask<Result<FavoriteResponseModel>> Get(int id)
    {
        return ApiClient.Instance.Get<FavoriteResponseModel>($"levels/{id}");
    }

    public static UniTask<Result<LevelsSearchResponseDTO>> Search(Action<LevelsSearchRequestDTOBuilder> builder)
    {
        LevelsSearchRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        LevelsSearchRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Get<LevelsSearchResponseDTO>($"levels/search?{dto.ToQueryString()}");
    }
}
