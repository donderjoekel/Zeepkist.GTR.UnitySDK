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
public class FavoritesApi
{
    public static UniTask<Result<FavoritesGetAllResponseDTO>> Get(
        Action<FavoritesGetAllRequestDTOBuilder> builder
    )
    {
        FavoritesGetAllRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        FavoritesGetAllRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Get<FavoritesGetAllResponseDTO>($"favorites?{dto.ToQueryString()}", false);
    }

    public static UniTask<Result<FavoriteResponseModel>> Get(int id)
    {
        return ApiClient.Instance.Get<FavoriteResponseModel>($"favorites/{id}");
    }

    public static UniTask<Result<GenericIdResponseDTO>> Add(Action<FavoritesAddRequestDTOBuilder> builder)
    {
        FavoritesAddRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        FavoritesAddRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Post<GenericIdResponseDTO>("favorites", dto);
    }

    public static UniTask<Result> Remove(Action<FavoritesRemoveRequestDTOBuilder> builder)
    {
        FavoritesRemoveRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        FavoritesRemoveRequestDTO dto = actualBuilder.Build();

        return ApiClient.Instance.Delete("favorites", dto);
    }
}
