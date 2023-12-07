using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

[PublicAPI]
public class RecordsApi : IRecordsApi
{
    private readonly Sdk sdk;

    public RecordsApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<RecordResponseModel>> GetById(int id)
    {
        return sdk.ApiClient.Get<RecordResponseModel>($"records/{id}");
    }

    public UniTask<Result<RecordsGetByIdsResponseDTO>> GetByIds(Action<RecordsGetByIdsRequestDTOBuilder> builder)
    {
        RecordsGetByIdsRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetByIdsRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<RecordsGetByIdsResponseDTO>($"records?{dto.ToQueryString()}");
    }

    public UniTask<Result<RecordsGetResponseDTO>> Get(Action<RecordsGetRequestDTOBuilder> builder)
    {
        RecordsGetRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<RecordsGetResponseDTO>($"records?{dto.ToQueryString()}");
    }

    public UniTask<Result<RecordsGetRecentResponseDTO>> GetRecent(
        Action<RecordsGetRecentRequestDTOBuilder> builder
    )
    {
        RecordsGetRecentRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsGetRecentRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<RecordsGetRecentResponseDTO>($"records/recent?{dto.ToQueryString()}");
    }

    public UniTask<Result> Submit(Action<RecordsSubmitRequestDTOBuilder> builder)
    {
        RecordsSubmitRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        RecordsSubmitRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Post("records/submit", dto);
    }
}
