using Microsoft.Extensions.Logging;

namespace Rst.Handlers.Response;

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
        var response = await base.SendAsync(request, cancellationToken);
        
        var str = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogInformation("Response status code {statusCode} response body {message}",
            response.StatusCode, str);
        return response;
    }
}