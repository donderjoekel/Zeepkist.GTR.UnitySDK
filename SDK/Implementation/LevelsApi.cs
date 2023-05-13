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
public class LevelsApi : ILevelsApi
{
    private readonly Sdk sdk;

    public LevelsApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<LevelsGetResponseDTO>> Get(Action<LevelsGetRequestDTOBuilder> builder)
    {
        LevelsGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        LevelsGetRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<LevelsGetResponseDTO>($"levels?{dto.ToQueryString()}");
    }

    public UniTask<Result<FavoriteResponseModel>> Get(int id)
    {
        return sdk.ApiClient.Get<FavoriteResponseModel>($"levels/{id}");
    }

    public UniTask<Result<LevelsSearchResponseDTO>> Search(Action<LevelsSearchRequestDTOBuilder> builder)
    {
        LevelsSearchRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        LevelsSearchRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<LevelsSearchResponseDTO>($"levels/search?{dto.ToQueryString()}");
    }

    public UniTask<Result<LevelsGetRandomResponseDTO>> Random(Action<GenericGetRequestDTOBuilder> builder)
    {
        GenericGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        GenericGetRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<LevelsGetRandomResponseDTO>($"levels/random?{dto.ToQueryString()}");
    }
}
