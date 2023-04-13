using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

internal abstract class ClientBase
{
    private readonly HttpClient httpClient;

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

    public async UniTask<Result<TResponse>> Get<TResponse>(
        string requestUri,
        bool addAuth = true,
        CancellationToken ct = default
    )
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Get<TResponse>(requestUri, addAuth, ct);
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
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Post(requestUri, data, addAuth, ct);
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
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Post<TResponse>(requestUri, data, addAuth, ct);
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
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Patch(requestUri, data, addAuth, ct);
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
        CancellationToken ct = default
    )
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Delete(requestUri, addAuth, ct);
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
        CancellationToken ct = default
    )
    {
        string requestJson = JsonConvert.SerializeObject(data);
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        requestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

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
            Result refreshAuthResult = await RefreshAuth();
            if (refreshAuthResult.IsSuccess)
            {
                return await Delete(requestUri, data, addAuth, ct);
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
