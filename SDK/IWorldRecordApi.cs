using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IWorldRecordApi
{
    UniTask<Result<WorldRecordGetGhostResponseDTO>> GetGhost(Action<WorldRecordGetGhostRequestDTOBuilder> builder);

    UniTask<Result<WorldRecordGetUiResponseDTO>> GetUi(Action<WorldRecordGetUiRequestDTOBuilder> builder);

    UniTask<Result<WorldRecordGetByLevelResponseDTO>> GetByLevel(
        Action<WorldRecordGetByLevelRequestDTOBuilder> builder
    );
}
