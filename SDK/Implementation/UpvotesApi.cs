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
public class UpvotesApi : IUpvotesApi
{
    private readonly Sdk sdk;
    
    public UpvotesApi(Sdk sdk)
    {
        this.sdk = sdk;
    }
    
    public UniTask<Result<UpvotesGetResponseDTO>> Get(Action<UpvotesGetRequestDTOBuilder> builder)
    {
        UpvotesGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UpvotesGetRequestDTO dto = b.Build();

        return sdk.ApiClient.Get<UpvotesGetResponseDTO>($"upvotes?{dto.ToQueryString()}");
    }

    public UniTask<Result<UpvoteResponseModel>> Get(int id)
    {
        return sdk.ApiClient.Get<UpvoteResponseModel>($"upvotes/{id}");
    }

    public UniTask<Result<GenericIdResponseDTO>> Add(int levelId)
    {
        return Add(builder => builder.WithLevelId(levelId));
    }

    public UniTask<Result<GenericIdResponseDTO>> Add(Action<UpvotesAddRequestDTOBuilder> builder)
    {
        UpvotesAddRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UpvotesAddRequestDTO dto = b.Build();

        return sdk.ApiClient.Post<GenericIdResponseDTO>("upvotes", dto);
    }

    public UniTask<Result> Remove(int id)
    {
        return Remove(b => b.WithId(id));
    }

    public UniTask<Result> Remove(Action<GenericIdRequestDTOBuilder> builder)
    {
        GenericIdRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericIdRequestDTO dto = b.Build();

        return sdk.ApiClient.Delete("upvotes", dto);
    }
}
