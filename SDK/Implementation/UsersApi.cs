using System;
using System.Linq;
using System.Net;
using System.Text;
using JetBrains.Annotations;
using Steamworks;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.RequestDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.DTOs.ResponseModels;
using TNRD.Zeepkist.GTR.FluentResults;
using TNRD.Zeepkist.GTR.SDK.Client;
using TNRD.Zeepkist.GTR.SDK.Errors;
using TNRD.Zeepkist.GTR.SDK.Models.Request;
using TNRD.Zeepkist.GTR.SDK.Models.Response;
using Result = TNRD.Zeepkist.GTR.FluentResults.Result;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public class UsersApi : IUsersApi
{
    private const string KEY_USER_ID = "GTR.UserId";
    private const string KEY_STEAM_ID = "GTR.SteamId";
    private const string KEY_ACCESS_TOKEN = "GTR.AccessToken";
    private const string KEY_REFRESH_TOKEN = "GTR.RefreshToken";
    private const string KEY_MOD_VERSION = "GTR.ModVersion";

    private readonly Sdk sdk;

    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    [Obsolete("Use UserId instead")]
    public int Id => UserId;

    /// <summary>
    /// Used to check if we are already in possession of all tokens and data
    /// </summary>
    private bool HasTokens => UserId >= 0 &&
                              !string.IsNullOrEmpty(AccessToken) &&
                              !string.IsNullOrEmpty(RefreshToken);

    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    public int UserId { get; private set; } = -1;

    private string SteamId => ((ulong)SteamClient.SteamId).ToString();

    /// <summary>
    /// The access token that is used for authentication 
    /// </summary>
    public string AccessToken { get; private set; } = null;

    /// <summary>
    /// The refresh token that is used for refreshing authentication
    /// </summary>
    private string RefreshToken { get; set; } = null;

    public string ModVersion { get; private set; } = null;

    public UsersApi(Sdk sdk)
    {
        this.sdk = sdk;
    }

    public async UniTask<Result<UsersGetAllResponseDTO>> Get(Action<GenericGetRequestDTOBuilder> builder)
    {
        GenericGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericGetRequestDTO dto = b.Build();

        return await sdk.ApiClient.Get<UsersGetAllResponseDTO>($"users?{dto.ToQueryString()}");
    }

    public async UniTask<Result<UserResponseModel>> GetById(Action<GenericIdRequestDTOBuilder> builder)
    {
        GenericIdRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericIdRequestDTO dto = b.Build();

        return await sdk.ApiClient.Get<UserResponseModel>($"users/{dto.ToQueryString()}");
    }

    public async UniTask<Result<UserResponseModel>> GetBySteamId(
        Action<UsersGetBySteamIdRequestDTOBuilder> builder
    )
    {
        UsersGetBySteamIdRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UsersGetBySteamIdRequestDTO dto = b.Build();

        return await sdk.ApiClient.Get<UserResponseModel>($"users/steam/{dto.ToQueryString()}");
    }

    public async UniTask<Result<UsersRankingResponseDTO>> Ranking(
        Action<GenericGetRequestDTOBuilder> builder
    )
    {
        GenericGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericGetRequestDTO dto = b.Build();

        return await sdk.ApiClient.Get<UsersRankingResponseDTO>($"users/ranking?{dto.ToQueryString()}");
    }

    public async UniTask<Result<UsersRankingsResponseDTO>> Rankings(Action<GenericGetRequestDTOBuilder> builder)
    {
        GenericGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericGetRequestDTO dto = b.Build();

        return await sdk.ApiClient.Get<UsersRankingsResponseDTO>($"users/rankings?{dto.ToQueryString()}");
    }

    /// <summary>
    /// Attempts to log in the user
    /// </summary>
    /// <returns></returns>
    public async UniTask<Result> Login(string modVersion, bool allowRefresh)
    {
        if (!SteamClient.IsLoggedOn)
        {
            return Result.Fail(new SteamNotLoggedOnError());
        }

        if (HasTokens && allowRefresh)
        {
            Result refreshResult = await Refresh(modVersion);
            if (refreshResult.IsSuccess)
                return Result.Ok();
        }

        LoginRequestModel loginRequestModel = new LoginRequestModel()
        {
            ModVersion = modVersion,
            AuthenticationTicket = CreateAuthenticationTicket(),
            SteamId = ((ulong)SteamClient.SteamId).ToString()
        };

        Result<LoginResponseModel> result =
            await sdk.AuthClient.Post<LoginResponseModel>("game/login", loginRequestModel, false);

        if (result.IsFailed)
            return result.ToResult();

        UserId = result.Value.UserId;
        AccessToken = result.Value.AccessToken;
        RefreshToken = result.Value.RefreshToken;
        ModVersion = modVersion;

        return Result.Ok();
    }

    private string CreateAuthenticationTicket()
    {
        AuthTicket authSessionTicket = SteamUser.GetAuthSessionTicket();
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < authSessionTicket.Data.Length; ++index)
        {
            stringBuilder.AppendFormat("{0:x2}", authSessionTicket.Data[index]);
        }

        return stringBuilder.ToString();
    }

    public async UniTask<Result> Refresh(string modVersion)
    {
        if (!SteamClient.IsLoggedOn)
        {
            return Result.Fail(new SteamNotLoggedOnError());
        }

        if (!HasTokens)
        {
            return Result.Fail("Missing tokens");
        }

        RefreshRequestModel refreshRequestModel = new RefreshRequestModel()
        {
            ModVersion = modVersion,
            RefreshToken = RefreshToken,
            SteamId = SteamId
        };

        Result<RefreshResponseModel> result =
            await sdk.AuthClient.Post<RefreshResponseModel>("game/refresh", refreshRequestModel, false, false, true);

        if (result.IsFailed)
        {
            StatusCodeReason reason = (StatusCodeReason)result.Reasons.FirstOrDefault(x => x is StatusCodeReason);
            if (reason == null)
                return result.ToResult();

            if (reason.StatusCode == HttpStatusCode.Unauthorized)
            {
                AccessToken = string.Empty;
                RefreshToken = string.Empty;
                return await Login(modVersion, false);
            }
        }

        if (result.IsFailed)
        {
            return result.ToResult();
        }

        UserId = result.Value.UserId;
        AccessToken = result.Value.AccessToken;
        RefreshToken = result.Value.RefreshToken;
        ModVersion = modVersion;

        return Result.Ok();
    }
}
