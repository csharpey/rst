using Microsoft.Extensions.Logging;

namespace Rst.Handlers.Response;

public class SuccessHandler : DelegatingHandler
{
    private readonly ILogger<SuccessHandler> _logger;

    public SuccessHandler(ILogger<SuccessHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage>
        SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        return response;
    }
}