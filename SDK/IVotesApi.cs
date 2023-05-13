using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IVotesApi
{
    UniTask<Result<VotesGetCategoriesResponseDTO>> Categories();
    UniTask<Result<VotesGetResponseDTO>> Get(Action<VotesGetRequestDTOBuilder> builder);
    UniTask<Result<VotesGetAverageResponseDTO>> Get(Action<VotesGetAverageRequestDTOBuilder> builder);
    UniTask<Result> Submit(Action<VotesSubmitRequestDTOBuilder> builder);
}
