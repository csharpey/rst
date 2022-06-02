using Microsoft.AspNetCore.Mvc;
using Rst.Auth.Utils.Filters;
using Rst.Auth.Utils.Requirements;

namespace Rst.Auth.Utils.Attributes;

public class RequireClaimAttribute : TypeFilterAttribute
{
    public RequireClaimAttribute(string type, string query, TreeFlags flags = TreeFlags.All) : base(
        typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new ClaimRequirement(type, query, flags) };
    }
}