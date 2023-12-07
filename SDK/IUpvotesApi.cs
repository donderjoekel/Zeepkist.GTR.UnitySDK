using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IUpvotesApi
{
    UniTask<Result<UpvotesGetResponseDTO>> Get(Action<UpvotesGetRequestDTOBuilder> builder);
    UniTask<Result<UpvoteResponseModel>> Get(int id);
    UniTask<Result<GenericIdResponseDTO>> Add(string level);
    UniTask<Result<GenericIdResponseDTO>> Add(Action<UpvotesAddRequestDTOBuilder> builder);
    UniTask<Result> Remove(int id);
}
