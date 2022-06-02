using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;
using Rst.Auth.Utils.Requirements;

namespace Rst.Auth.Utils.Handlers;

public class InMemoryClaimRequirementHandler : AuthorizationHandler<ClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
    {
        var tree = context.User.Claims.Where(c => c.Type == requirement.Type)
            .Select(c => new LTree(c.Value))
            .ToImmutableArray();

        var success = false;
        if (requirement.Flags.HasFlag(TreeFlags.Child))
        {
            success |= tree.Any(t => new LTree(requirement.Query).IsAncestorOf(t));
        }

        if (requirement.Flags.HasFlag(TreeFlags.Pattern))
        {
            success |= tree.Any(t => t.MatchesLQuery(requirement.Query));
        }
        if (!success)
        {
            context.Fail(new AuthorizationFailureReason(this, "Claims forbid"));
        }

        return Task.CompletedTask;
    }
}