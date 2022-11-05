using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Rst.Handlers.Interfaces;

namespace Rst.Handlers.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ClientCredentialsTokenRequest>(configuration.GetSection("Handlers:TokenRequest"));
        services.AddScoped<IAuthenticationStorage, JwtAuthenticationStorage>();
        services.AddHeaderPropagation(options =>
        {
            options.Headers.Add(HeaderNames.Authorization,
                context =>
                {
                    var service = context.HttpContext.RequestServices
                        .GetRequiredService<IAuthenticationStorage>();
                    var token = service.TokenAsync(context.HttpContext.RequestAborted)
                        .GetAwaiter().GetResult();
                    return new StringValues(token.ToString());
                });
            options.Headers.Add("Accept-Language");
        });
    }
}