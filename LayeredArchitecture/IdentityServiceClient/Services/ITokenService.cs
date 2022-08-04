using IdentityServiceClient.Models;

namespace IdentityServiceClient.Services;

public interface ITokenService
{
    string GenerateToken(User user);

    bool IsTokenValid(string token);

    string GetClaim(string token, string claimName);
}