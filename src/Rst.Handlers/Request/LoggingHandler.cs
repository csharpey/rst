using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging;

namespace Rst.Handlers.Request;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request {Request}", request);
        if (request.Content != null)
        {
            _logger.LogInformation(await request.Content.ReadAsStringAsync(cancellationToken));
        }

        return await base.SendAsync(request, cancellationToken);
    }
}