using System.Net;
using Rst.Handlers.Interfaces;

namespace Rst.Handlers.Request;

public class AuthHandler : DelegatingHandler
{
    private readonly IAuthenticationStorage _authenticationStorage;

    public AuthHandler(IAuthenticationStorage authenticationStorage)
    {
        _authenticationStorage = authenticationStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        await _authenticationStorage.Pass(request);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized) return response;
        
        await _authenticationStorage.Refresh(request);
        response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}