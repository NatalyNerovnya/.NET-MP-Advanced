namespace IdentityServiceClient.Models;

public class Role
{
    public string Name { get; set; }

    public IList<Permissions> Permissions { get; set; }
}