using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Rst.Auth.Utils.Requirements;

namespace Rst.Auth.Utils.Filters;

public class ClaimRequirementFilter : IAsyncAuthorizationFilter
{
    private readonly ClaimRequirement[] _requirements;

    public ClaimRequirementFilter(ClaimRequirement requirement)
    {
        _requirements = new[] { requirement };
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var handler = context.HttpContext.RequestServices.GetServices<IAuthorizationHandler>()
            .First(h => h is AuthorizationHandler<ClaimRequirement>);
        var c = new AuthorizationHandlerContext(_requirements, context.HttpContext.User, null);
        await handler.HandleAsync(c);

        if (c.HasFailed)
        {
            context.Result = new ForbidResult();
        }
    }
}