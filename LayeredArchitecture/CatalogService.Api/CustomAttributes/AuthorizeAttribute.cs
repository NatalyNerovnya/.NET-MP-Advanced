using CatalogService.Api.Models;
using IdentityServiceClient.Models;
using IdentityServiceClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CatalogService.Api.CustomAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _access;

    public AuthorizeAttribute(string access)
    {
        _access = access;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            return;
        }

        var rolePermissions = JsonConvert.DeserializeObject<List<Permissions>>(context.HttpContext.Items["permissions"].ToString());
        var allowedAccess = GetPermissions();
        if (!allowedAccess.Any() || rolePermissions is null || !rolePermissions.Any() || !allowedAccess.All(x => rolePermissions.Contains(x)))
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

    private IList<Permissions> GetPermissions()
    {
        if (string.Equals(_access, "Read", StringComparison.InvariantCultureIgnoreCase))
        {
            return AccessPolicies.Read;
        }
        else if (string.Equals(_access, "ReadWrite", StringComparison.InvariantCultureIgnoreCase))
        {
            return AccessPolicies.ReadWrite;
        }

        return new List<Permissions>();
    }
}