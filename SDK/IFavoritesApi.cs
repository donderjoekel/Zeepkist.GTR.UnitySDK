using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IFavoritesApi
{
    UniTask<Result<FavoritesGetAllResponseDTO>> Get(
        Action<FavoritesGetAllRequestDTOBuilder> builder
    );

    UniTask<Result<FavoriteResponseModel>> Get(int id);
    UniTask<Result<GenericIdResponseDTO>> Add(string level);
    UniTask<Result<GenericIdResponseDTO>> Add(Action<FavoritesAddRequestDTOBuilder> builder);
    UniTask<Result> Remove(int id);
}
