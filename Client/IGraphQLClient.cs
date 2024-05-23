using System.Threading;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

public interface IGraphQLClient
{
    UniTask<Result<TResponse>> Post<TResponse>(
        object data,
        CancellationToken ct = default
    );
}
