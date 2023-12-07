using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IPersonalBestApi
{
    UniTask<Result<PersonalBestGetGhostResponseDTO>> GetGhost(Action<PersonalBestGetGhostRequestDTOBuilder> builder);

    UniTask<Result<PersonalBestGetUiResponseDTO>> GetUi(Action<PersonalBestGetUiRequestDTOBuilder> builder);

    UniTask<Result<PersonalBestGetLeaderboardResponseDTO>> GetLeaderboard(
        Action<PersonalBestGetLeaderboardRequestDTOBuilder> builder
    );

    UniTask<Result<PersonalBestGetByLevelResponseDTO>> GetByLevel(
        Action<PersonalBestGetByLevelRequestDTOBuilder> builder
    );

    UniTask<Result<PersonalBestGetByLevelAndUserResponseDTO>> GetByLevelAndUser(
        Action<PersonalBestGetByLevelAndUserRequestDTOBuilder> builder
    );

    UniTask<Result<PersonalBestGetByLevelResponseDTO>> GetFastestByLevel(
        Action<PersonalBestGetByLevelRequestDTOBuilder> builder
    );
}
