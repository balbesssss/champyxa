using System.Security.Principal;

namespace api.DTO;

public class JWTToken
{
    public string Token { get; set; }
    public string AccessToken { get; set; }
}