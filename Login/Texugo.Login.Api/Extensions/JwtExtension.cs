using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Texugo.Login.Core;
using Texugo.Login.Core.Contexts.AccountContext.UseCases.Authenticate;

namespace Texugo.Login.Api.Extensions;

public static class JwtExtension
{
    public static string Generate(ResponseData data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(ResponseData user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim("id", user.Id));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in user.Roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        
        return ci;
    }

}