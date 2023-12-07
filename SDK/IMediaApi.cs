using TNRD.Zeepkist.GTR.Cysharp.Threading.Tasks;
using TNRD.Zeepkist.GTR.DTOs.ResponseDTOs;
using TNRD.Zeepkist.GTR.FluentResults;

namespace TNRD.Zeepkist.GTR.SDK;

public interface IMediaApi
{
    UniTask<Result<MediaGetByRecordResponseDTO>> GetByRecord(int id);
}
