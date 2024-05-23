using System.Threading;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class ZworpClient : ClientBase, IZworpClient
{
    public static ZworpClient Create(Sdk sdk, string baseAddress, bool logRequestUrl, bool logResponseOutput)
    {
        ZworpClient instance = new(sdk, baseAddress);
        instance.LogRequestUrl = logRequestUrl;
        instance.LogResponseOutput = logResponseOutput;
        return instance;
    }

    private ZworpClient(Sdk sdk, string baseAddress)
        : base(sdk, baseAddress)
    {
    }

    UniTask<Result> IZworpClient.Get(string requestUri, CancellationToken ct)
    {
        return Get(requestUri, false, false, ct);
    }

    UniTask<Result<TResponse>> IZworpClient.Get<TResponse>(string requestUri, CancellationToken ct)
    {
        return Get<TResponse>(requestUri, false, false, true, ct);
    }

    UniTask<Result> IZworpClient.Post(string requestUri, object data, CancellationToken ct)
    {
        return Post(requestUri, data, false, false, ct);
    }

    UniTask<Result<TResponse>> IZworpClient.Post<TResponse>(string requestUri, object data, CancellationToken ct)
    {
        return Post<TResponse>(requestUri, data, false, false, true, ct);
    }
}
