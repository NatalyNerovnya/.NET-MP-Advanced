using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Services;

public class TokenService : ITokenService
{
    private readonly string _secret;

    public TokenService(string secret)
    {
        _secret = secret;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("name", user.Name),
                new Claim("role", user.RoleName),
                new Claim("password", user.Password)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsTokenValid(string token)
    {
        if (token == null)
        {
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userName = jwtToken.Claims.First(x => x.Type == "name").Value;

            return !string.IsNullOrEmpty(userName);
        }
        catch
        {
            return false;
        }
    }
}