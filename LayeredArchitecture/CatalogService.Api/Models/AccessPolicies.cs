using IdentityServiceClient.Models;

namespace CatalogService.Api.Models;

public static class AccessPolicies
{
    public static readonly IList<Permissions> Read = new List<Permissions>() { Permissions.Read };

    public static readonly IList<Permissions> ReadWrite = new List<Permissions>() { Permissions.Read, Permissions.Create, Permissions.Delete, Permissions.Update };
}