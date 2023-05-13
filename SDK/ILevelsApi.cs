using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface ILevelsApi
{
    UniTask<Result<LevelsGetResponseDTO>> Get(Action<LevelsGetRequestDTOBuilder> builder);
    UniTask<Result<FavoriteResponseModel>> Get(int id);
    UniTask<Result<LevelsSearchResponseDTO>> Search(Action<LevelsSearchRequestDTOBuilder> builder);
    UniTask<Result<LevelsGetRandomResponseDTO>> Random(Action<GenericGetRequestDTOBuilder> builder);
}
