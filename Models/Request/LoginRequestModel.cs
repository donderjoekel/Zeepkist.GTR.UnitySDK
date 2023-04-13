namespace TNRD.Zeepkist.GTR.SDK.Models.Request;

internal class LoginRequestModel
{
    public string ModVersion { get; set; }
    public string SteamId { get; set; }
    public string AuthenticationTicket { get; set; }
}
