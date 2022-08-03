using IdentityServer.Models;

namespace IdentityServer.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);

    bool IsTokenValid(string token);
}