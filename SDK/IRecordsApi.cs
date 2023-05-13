using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IRecordsApi
{
    UniTask<Result<RecordsGetResponseDTO>> Get(Action<RecordsGetRequestDTOBuilder> builder);

    UniTask<Result<RecordsGetRecentResponseDTO>> GetRecent(
        Action<RecordsGetRecentRequestDTOBuilder> builder
    );
}
