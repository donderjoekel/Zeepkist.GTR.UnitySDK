using System;
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
using TNRD.Zeepkist.GTR.SDK.Models;
using TNRD.Zeepkist.GTR.SDK.Models.Request;
using TNRD.Zeepkist.GTR.SDK.Models.Response;
using UnityEngine;
using Result = TNRD.Zeepkist.GTR.FluentResults.Result;

namespace TNRD.Zeepkist.GTR.SDK;

[PublicAPI]
public static class UsersApi
{
    private const string KEY_USER_ID = "GTR.UserId";
    private const string KEY_STEAM_ID = "GTR.SteamId";
    private const string KEY_ACCESS_TOKEN = "GTR.AccessToken";
    private const string KEY_REFRESH_TOKEN = "GTR.RefreshToken";
    private const string KEY_MOD_VERSION = "GTR.ModVersion";

    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    [Obsolete("Use UserId instead")]
    public static int Id => UserId;

    /// <summary>
    /// Used to check if we are already in possession of all tokens and data
    /// </summary>
    private static bool HasTokens => UserId >= 0 &&
                                     !string.IsNullOrEmpty(AccessToken) &&
                                     !string.IsNullOrEmpty(RefreshToken);

    /// <summary>
    /// The user id that matches the local user in the database
    /// </summary>
    public static int UserId
    {
        get => PlayerPrefs.GetInt(KEY_USER_ID, -1);
        private set
        {
            PlayerPrefs.SetInt(KEY_USER_ID, value);
            PlayerPrefs.Save();
        }
    }

    private static string SteamId
    {
        get => PlayerPrefs.GetString(KEY_STEAM_ID, string.Empty);
        set
        {
            PlayerPrefs.SetString(KEY_STEAM_ID, value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// The access token that is used for authentication 
    /// </summary>
    internal static string AccessToken
    {
        get => PlayerPrefs.GetString(KEY_ACCESS_TOKEN, string.Empty);
        set
        {
            PlayerPrefs.SetString(KEY_ACCESS_TOKEN, value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// The refresh token that is used for refreshing authentication
    /// </summary>
    private static string RefreshToken
    {
        get => PlayerPrefs.GetString(KEY_REFRESH_TOKEN, string.Empty);
        set
        {
            PlayerPrefs.SetString(KEY_REFRESH_TOKEN, value);
            PlayerPrefs.Save();
        }
    }

    internal static string ModVersion
    {
        get => PlayerPrefs.GetString(KEY_MOD_VERSION, string.Empty);
        set
        {
            PlayerPrefs.SetString(KEY_MOD_VERSION, value);
            PlayerPrefs.Save();
        }
    }

    public static async UniTask<Result<UsersGetAllResponseDTO>> Get(Action<GenericGetRequestDTOBuilder> builder)
    {
        GenericGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericGetRequestDTO dto = b.Build();

        return await ApiClient.Instance.Get<UsersGetAllResponseDTO>($"users?{dto.ToQueryString()}");
    }

    public static async UniTask<Result<UserResponseModel>> GetById(Action<GenericIdRequestDTOBuilder> builder)
    {
        GenericIdRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericIdRequestDTO dto = b.Build();

        return await ApiClient.Instance.Get<UserResponseModel>($"users/{dto.ToQueryString()}");
    }

    public static async UniTask<Result<UserResponseModel>> GetBySteamId(
        Action<UsersGetBySteamIdRequestDTOBuilder> builder
    )
    {
        UsersGetBySteamIdRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UsersGetBySteamIdRequestDTO dto = b.Build();

        return await ApiClient.Instance.Get<UserResponseModel>($"users/steam/{dto.ToQueryString()}");
    }

    public static async UniTask<Result<UsersRankingResponseDTO>> Ranking(
        Action<UsersRankingGetRequestDTOBuilder> builder
    )
    {
        UsersRankingGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        UsersRankingGetRequestDTO dto = b.Build();

        return await ApiClient.Instance.Get<UsersRankingResponseDTO>($"users/ranking?{dto.ToQueryString()}");
    }

    public static async UniTask<Result<UsersRankingsResponseDTO>> Rankings(Action<GenericGetRequestDTOBuilder> builder)
    {
        GenericGetRequestDTOBuilder b = new();
        builder?.Invoke(b);
        GenericGetRequestDTO dto = b.Build();

        return await ApiClient.Instance.Get<UsersRankingsResponseDTO>($"users/rankings?{dto.ToQueryString()}");
    }

    /// <summary>
    /// Attempts to log in the user
    /// </summary>
    /// <returns></returns>
    public static async UniTask<Result> Login(string modVersion)
    {
        if (!SteamClient.IsLoggedOn)
        {
            return Result.Fail(new SteamNotLoggedOnError());
        }

        if (HasTokens)
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
            await AuthClient.Instance.Post<LoginResponseModel>("game/login", loginRequestModel, false);

        if (result.IsFailed)
            return result.ToResult();

        UserId = result.Value.UserId;
        SteamId = result.Value.SteamId;
        AccessToken = result.Value.AccessToken;
        RefreshToken = result.Value.RefreshToken;
        ModVersion = modVersion;

        return Result.Ok();
    }

    private static string CreateAuthenticationTicket()
    {
        AuthTicket authSessionTicket = SteamUser.GetAuthSessionTicket();
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < authSessionTicket.Data.Length; ++index)
        {
            stringBuilder.AppendFormat("{0:x2}", authSessionTicket.Data[index]);
        }

        return stringBuilder.ToString();
    }

    internal static async UniTask<Result> Refresh(string modVersion)
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
            await AuthClient.Instance.Post<RefreshResponseModel>("game/refresh", refreshRequestModel, false);

        if (result.IsFailed)
            return result.ToResult();

        UserId = result.Value.UserId;
        SteamId = result.Value.SteamId;
        AccessToken = result.Value.AccessToken;
        RefreshToken = result.Value.RefreshToken;
        ModVersion = modVersion;

        return Result.Ok();
    }

    /// <summary>
    /// Attempts to update the name of the current player
    /// </summary>
    /// <returns></returns>
    public static async UniTask<Result> UpdateName()
    {
        if (!SteamClient.IsLoggedOn)
        {
            return Result.Fail(new SteamNotLoggedOnError());
        }

        UsersUpdateNameRequestDTO requestDTO = new UsersUpdateNameRequestDTOBuilder()
            .WithSteamName(SteamClient.Name)
            .Build();

        return await ApiClient.Instance.Post("users/name", requestDTO);
    }
}
