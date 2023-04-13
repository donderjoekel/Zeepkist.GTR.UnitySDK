namespace TNRD.Zeepkist.GTR.SDK.Models.Request;

internal class RefreshRequestModel
{
    public string ModVersion { get; set; } = null!;
    public string SteamId { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
