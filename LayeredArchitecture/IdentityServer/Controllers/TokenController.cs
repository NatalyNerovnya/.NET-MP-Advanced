using IdentityServiceClient.Models;
using IdentityServiceClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[ApiController]
public class TokenController: ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("token")]
    public ActionResult<string> Generate(User user)
    {
        var token = _tokenService.GenerateToken(user);
        return Ok(token);
    }

    [HttpGet("token/{token}")]
    public ActionResult<bool> Validate(string token)
    {
        var isValid = _tokenService.IsTokenValid(token);
        return Ok(isValid);
    }
}