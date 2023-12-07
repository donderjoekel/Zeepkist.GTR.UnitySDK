using System;
using System.Collections.Generic;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IRecordsApi
{
    UniTask<Result<RecordResponseModel>> GetById(int id);
    UniTask<Result<RecordsGetByIdsResponseDTO>> GetByIds(Action<RecordsGetByIdsRequestDTOBuilder> builder);
    UniTask<Result<RecordsGetResponseDTO>> Get(Action<RecordsGetRequestDTOBuilder> builder);
    UniTask<Result<RecordsGetRecentResponseDTO>> GetRecent(Action<RecordsGetRecentRequestDTOBuilder> builder);
    UniTask<Result> Submit(Action<RecordsSubmitRequestDTOBuilder> builder);
}
