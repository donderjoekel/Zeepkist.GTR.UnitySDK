using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

internal class PersonalBestApi : IPersonalBestApi
{
    private readonly Sdk sdk;

    public PersonalBestApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<PersonalBestGetGhostResponseDTO>> GetGhost(
        Action<PersonalBestGetGhostRequestDTOBuilder> builder
    )
    {
        PersonalBestGetGhostRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetGhostRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetGhostResponseDTO>($"pbs/ghost?{dto.ToQueryString()}",
            false,
            true,
            true);
    }

    public UniTask<Result<PersonalBestGetUiResponseDTO>> GetUi(Action<PersonalBestGetUiRequestDTOBuilder> builder)
    {
        PersonalBestGetUiRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetUiRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetUiResponseDTO>($"pbs/ui?{dto.ToQueryString()}", false, true, true);
    }

    public UniTask<Result<PersonalBestGetLeaderboardResponseDTO>> GetLeaderboard(
        Action<PersonalBestGetLeaderboardRequestDTOBuilder> builder
    )
    {
        PersonalBestGetLeaderboardRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetLeaderboardRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetLeaderboardResponseDTO>($"pbs/leaderboard?{dto.ToQueryString()}",
            false,
            true,
            true);
    }

    public UniTask<Result<PersonalBestGetByLevelResponseDTO>> GetByLevel(
        Action<PersonalBestGetByLevelRequestDTOBuilder> builder
    )
    {
        PersonalBestGetByLevelRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetByLevelRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetByLevelResponseDTO>($"pbs/level/{dto.Level}", false, true, true);
    }

    public UniTask<Result<PersonalBestGetByLevelAndUserResponseDTO>> GetByLevelAndUser(
        Action<PersonalBestGetByLevelAndUserRequestDTOBuilder> builder
    )
    {
        PersonalBestGetByLevelAndUserRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetByLevelAndUserRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetByLevelAndUserResponseDTO>($"pbs/level/{dto.Level}/user/{dto.User}",
            false,
            true,
            true);
    }

    public UniTask<Result<PersonalBestGetByLevelResponseDTO>> GetFastestByLevel(
        Action<PersonalBestGetByLevelRequestDTOBuilder> builder
    )
    {
        PersonalBestGetByLevelRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        PersonalBestGetByLevelRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<PersonalBestGetByLevelResponseDTO>($"pbs/fastest/level?{dto.ToQueryString()}",
            false,
            true,
            true);
    }
}
