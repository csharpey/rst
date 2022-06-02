using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rst.Auth.Utils.Handlers;
using Rst.Auth.Utils.Options;

namespace Rst.Auth.Utils.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNpgsqlClaimRequirements(this IServiceCollection services, Action<ClaimRequirementsOptions> configure)
    {
        services.Configure(configure);
        services.AddSingleton<IAuthorizationHandler, NpgsqlClaimRequirementHandler>();
        return services;
    }
}