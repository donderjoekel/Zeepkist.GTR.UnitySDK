using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;
using UnityEngine;

namespace TNRD.Zeepkist.GTR.SDK.Client;

internal abstract class ClientBase
{
    private readonly HttpClient httpClient;

    protected bool LogRequestUrl { get; set; }
    protected bool LogResponseOutput { get; set; }

    protected ClientBase(string baseAddress)
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseAddress);
    }

    private static async UniTask<Result> RefreshAuth()
    {
        Result refreshResult = await UsersApi.Refresh(UsersApi.ModVersion);
        if (refreshResult.IsSuccess)
            return Result.Ok();

        Result loginResult = await UsersApi.Login(UsersApi.ModVersion);
        if (loginResult.IsSuccess)
            return Result.Ok();

        return Result.Merge(refreshResult, loginResult);
    }

    private void LogUrl(HttpRequestMessage msg)
    {
        if (LogRequestUrl)
            Debug.Log($"[GTR][{msg.Method.Method}] {msg.RequestUri}");
    }

    private void LogContent(HttpRequestMessage msg, string content)
    {
        if (LogResponseOutput)
            Debug.Log($"[GTR][{msg.Method.Method}] {msg.RequestUri}\nRESPONSE:\n{content}");
    }

    public async UniTask<Result<TResponse>> Get<TResponse>(
        string requestUri,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Get<TResponse>(requestUri, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        try
        {
            string responseJson = await response.Content.ReadAsStringAsync();
            LogContent(requestMessage, responseJson);
            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }
        catch (Exception e)
        {
            return new ExceptionalError(e);
        }
    }

    public async UniTask<Result> Post(
        string requestUri,
        object data,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Post(requestUri, data, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        return Result.Ok()
            .WithReason(new StatusCodeReason(response.StatusCode));
    }

    public async UniTask<Result<TResponse>> Post<TResponse>(
        string requestUri,
        object data,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Post<TResponse>(requestUri, data, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        try
        {
            string responseJson = await response.Content.ReadAsStringAsync();
            LogContent(requestMessage, responseJson);
            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }
    }

    public async UniTask<Result> Patch(
        string requestUri,
        object data,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Patch(requestUri, data, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        return Result.Ok()
            .WithReason(new StatusCodeReason(response.StatusCode));
    }

    public async UniTask<Result> Delete(
        string requestUri,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Delete(requestUri, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        return Result.Ok()
            .WithReason(new StatusCodeReason(response.StatusCode));
    }

    public async UniTask<Result> Delete(
        string requestUri,
        object data,
        bool addAuth = true,
        bool allowRefresh = true,
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        LogUrl(requestMessage);

        if (addAuth)
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", UsersApi.AccessToken);

        HttpResponseMessage response = null;

        try
        {
            response = await httpClient.SendAsync(requestMessage, ct);
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e));
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (!allowRefresh)
            {
                return Result.Fail("Unauthorized")
                    .WithReason(new StatusCodeReason(response.StatusCode));
            }

            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Delete(requestUri, data, addAuth, false, ct);
            }
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            return Result.Fail(new ExceptionalError(e))
                .WithReason(new StatusCodeReason(response.StatusCode));
        }

        return Result.Ok()
            .WithReason(new StatusCodeReason(response.StatusCode));
    }
}
