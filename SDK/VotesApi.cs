using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;
using TNRD.Zeepkist.GTR.SDK.Models.Response;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class VotesApi
{
    public static UniTask<Result<VotesGetCategoriesResponseDTO>> Categories()
    {
        return ApiClient.Instance.Get<VotesGetCategoriesResponseDTO>("votes/categories");
    }

    public static UniTask<Result<VotesGetResponseDTO>> Get(Action<VotesGetRequestDTOBuilder> builder)
    {
        VotesGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesGetRequestDTO dto = b.Build();

        return ApiClient.Instance.Get<VotesGetResponseDTO>($"votes?{dto.ToQueryString()}");
    }

    public static UniTask<Result<VotesGetAverageResponseDTO>> Get(Action<VotesGetAverageRequestDTOBuilder> builder)
    {
        VotesGetAverageRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesGetAverageRequestDTO dto = b.Build();

        return ApiClient.Instance.Get<VotesGetAverageResponseDTO>($"votes/average?{dto.ToQueryString()}");
    }

    public static UniTask<Result> Submit(Action<VotesSubmitRequestDTOBuilder> builder)
    {
        VotesSubmitRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesSubmitRequestDTO dto = b.Build();
        
        return ApiClient.Instance.Post("votes", dto);
    }
}
