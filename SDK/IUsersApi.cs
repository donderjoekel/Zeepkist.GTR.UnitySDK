using System;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IUsersApi
{
    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    int Id { get; }

    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    int UserId { get; }

    /// <summary>
    /// The access token that is used for authentication 
    /// </summary>
    string AccessToken { get; }

    string ModVersion { get; }

    UniTask<Result<UsersGetAllResponseDTO>> Get(Action<GenericGetRequestDTOBuilder> builder);
    UniTask<Result<UserResponseModel>> GetById(Action<GenericIdRequestDTOBuilder> builder);

    UniTask<Result<UserResponseModel>> GetBySteamId(
        Action<UsersGetBySteamIdRequestDTOBuilder> builder
    );

    UniTask<Result<UsersRankingResponseDTO>> Ranking(
        Action<GenericGetRequestDTOBuilder> builder
    );

    UniTask<Result<UsersRankingsResponseDTO>> Rankings(Action<GenericGetRequestDTOBuilder> builder);

    /// <summary>
    /// Attempts to log in the user
    /// </summary>
    /// <returns></returns>
    UniTask<Result> Login(string modVersion, bool allowRefresh);

    UniTask<Result> Refresh(string modVersion);
}
