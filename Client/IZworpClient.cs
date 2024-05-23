using System.Threading;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

public interface IZworpClient
{
    UniTask<Result> Get(
        string requestUri,
        CancellationToken ct = default
    );

    UniTask<Result<TResponse>> Get<TResponse>(
        string requestUri,
        CancellationToken ct = default
    );

    UniTask<Result> Post(
        string requestUri,
        object data,
        CancellationToken ct = default
    );

    UniTask<Result<TResponse>> Post<TResponse>(
        string requestUri,
        object data,
        CancellationToken ct = default
    );
}
