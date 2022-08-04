using IdentityServiceClient.Services;
using Newtonsoft.Json;

namespace CatalogService.Api.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;


    public JwtMiddleware(RequestDelegate next, ITokenService tokenService, IRoleService roleService)
    {
        _next = next;
        _tokenService = tokenService;
        _roleService = roleService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await AttachRole(context, token);
        }

        await _next(context);
    }

    private async Task AttachRole(HttpContext context, string token)
    {
        var isValid = _tokenService.IsTokenValid(token);
        if (!isValid)
        {
            return;
        }

        var roleName = _tokenService.GetClaim(token, "role");
        var roles = await _roleService.GetAll();
        var role = roles.FirstOrDefault(x => x.Name.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
        context.Items["permissions"] = JsonConvert.SerializeObject(role.Permissions);
    }
}