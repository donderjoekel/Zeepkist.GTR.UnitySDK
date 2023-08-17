using System;
using JetBrains.Annotations;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

[PublicAPI]
public class VotesApi : IVotesApi
{
    private readonly Sdk sdk;

    public VotesApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<VotesGetCategoriesResponseDTO>> Categories()
    {
        return sdk.ApiClient.Get<VotesGetCategoriesResponseDTO>("votes/categories");
    }

    public UniTask<Result<VotesGetResponseDTO>> Get(Action<VotesGetRequestDTOBuilder> builder)
    {
        VotesGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesGetRequestDTO dto = b.Build();

        return sdk.ApiClient.Get<VotesGetResponseDTO>($"votes?{dto.ToQueryString()}");
    }

    public UniTask<Result<VotesGetAverageResponseDTO>> Get(Action<VotesGetAverageRequestDTOBuilder> builder)
    {
        VotesGetAverageRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesGetAverageRequestDTO dto = b.Build();

        return sdk.ApiClient.Get<VotesGetAverageResponseDTO>($"votes/average?{dto.ToQueryString()}");
    }

    public UniTask<Result> Submit(Action<VotesSubmitRequestDTOBuilder> builder)
    {
        VotesSubmitRequestDTOBuilder b = new();
        builder?.Invoke(b);
        VotesSubmitRequestDTO dto = b.Build();

        return sdk.ApiClient.Post("votes", dto);
    }
}
