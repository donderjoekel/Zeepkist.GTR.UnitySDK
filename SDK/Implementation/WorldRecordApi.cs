using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Implementation;

internal class WorldRecordApi : IWorldRecordApi
{
    private readonly Sdk sdk;

    public WorldRecordApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public UniTask<Result<WorldRecordGetGhostResponseDTO>> GetGhost(
        Action<WorldRecordGetGhostRequestDTOBuilder> builder
    )
    {
        WorldRecordGetGhostRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        WorldRecordGetGhostRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<WorldRecordGetGhostResponseDTO>($"wrs/ghost?{dto.ToQueryString()}", false, true, true);
    }

    public UniTask<Result<WorldRecordGetUiResponseDTO>> GetUi(Action<WorldRecordGetUiRequestDTOBuilder> builder)
    {
        WorldRecordGetUiRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        WorldRecordGetUiRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<WorldRecordGetUiResponseDTO>($"wrs/ui?{dto.ToQueryString()}", false, true, true);
    }

    public UniTask<Result<WorldRecordGetByLevelResponseDTO>> GetByLevel(
        Action<WorldRecordGetByLevelRequestDTOBuilder> builder
    )
    {
        WorldRecordGetByLevelRequestDTOBuilder actualBuilder = new();
        builder?.Invoke(actualBuilder);
        WorldRecordGetByLevelRequestDTO dto = actualBuilder.Build();

        return sdk.ApiClient.Get<WorldRecordGetByLevelResponseDTO>($"wrs/level/{dto.Level}", false, true, true);
    }
}
