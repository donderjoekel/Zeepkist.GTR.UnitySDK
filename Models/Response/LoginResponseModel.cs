using System;

namespace TNRD.Zeepkist.GTR.SDK.Models.Response;

internal class LoginResponseModel
{
    public int UserId { get; set; }
    public string SteamId { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public DateTime AccessExpiry { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshExpiry { get; set; }
}
