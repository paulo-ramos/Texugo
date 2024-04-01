using System.Security.Claims;

namespace Texugo.Login.Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string Id(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? string.Empty;
    
    public static string Name(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
    
    public static string GivenName(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    
    public static string Email(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
}