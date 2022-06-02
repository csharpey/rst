using Microsoft.AspNetCore.Authorization;
using Rst.Auth.Utils.Requirements;

namespace Rst.Auth.Utils.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequirePerm(this AuthorizationPolicyBuilder builder, string claimType,
        params string[] allowedValues)
    {
        if (claimType == null)
        {
            throw new ArgumentNullException(nameof(claimType));
        }

        foreach (var value in allowedValues)
        {
            builder.Requirements.Add(new ClaimRequirement(claimType, value));
        }

        return builder;
    }
}