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
public class UpvotesApi
{
    public static UniTask<Result<UpvotesGetResponseDTO>> Get(Action<UpvotesGetRequestDTOBuilder> builder)
    {
        UpvotesGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UpvotesGetRequestDTO dto = b.Build();

        return ApiClient.Instance.Get<UpvotesGetResponseDTO>($"upvotes?{dto.ToQueryString()}");
    }

    public static UniTask<Result<UpvoteResponseModel>> Get(int id)
    {
        return ApiClient.Instance.Get<UpvoteResponseModel>($"upvotes/{id}");
    }

    public static UniTask<Result<GenericIdResponseDTO>> Add(Action<UpvotesAddRequestDTOBuilder> builder)
    {
        UpvotesAddRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UpvotesAddRequestDTO dto = b.Build();

        return ApiClient.Instance.Post<GenericIdResponseDTO>("upvotes", dto);
    }

    public static UniTask<Result> Remove(Action<UpvotesRemoveRequestDTOBuilder> builder)
    {
        UpvotesRemoveRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UpvotesRemoveRequestDTO dto = b.Build();
        
        return ApiClient.Instance.Delete("upvotes", dto);
    }
}
