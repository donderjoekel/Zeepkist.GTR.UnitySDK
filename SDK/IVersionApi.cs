using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IVersionApi
{
    UniTask<Result<VersionInfo>> GetVersionInfo();
}
