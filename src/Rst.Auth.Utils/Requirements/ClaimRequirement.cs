using Microsoft.AspNetCore.Authorization;

namespace Rst.Auth.Utils.Requirements;

public class ClaimRequirement : IAuthorizationRequirement
{
    public ClaimRequirement(string type, string query, TreeFlags treeFlags = TreeFlags.All)
    {
        Type = type;
        Query = query;
        Flags = treeFlags;
    }
    
    public string Type { get; set; }
    public TreeFlags Flags { get; set; }
    public string Query { get; set; }
}