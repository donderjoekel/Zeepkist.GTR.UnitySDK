using System.Threading;
using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK.Client;

internal class GraphQLClient : ClientBase, IGraphQLClient
{
    public static GraphQLClient Create(Sdk sdk, string baseAddress)
    {
        return new GraphQLClient(sdk, baseAddress);
    }

    private GraphQLClient(Sdk sdk, string baseAddress)
        : base(sdk, baseAddress)
    {
    }

    public UniTask<Result<TResponse>> Post<TResponse>(object data, CancellationToken ct = default)
    {
        return base.Post<TResponse>(string.Empty, data, false, false, true, ct);
    }
}
