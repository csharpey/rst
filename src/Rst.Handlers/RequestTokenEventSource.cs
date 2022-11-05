using System.Diagnostics.Tracing;

namespace Rst.Handlers;

[EventSource(Name = "Rst.Handlers.Events")]
public sealed class RequestTokenEventSource : EventSource
{
    public static RequestTokenEventSource Log { get; } = new RequestTokenEventSource();

    private EventCounter? _requestCounter;

    private RequestTokenEventSource()
    {
        _requestCounter = new EventCounter("request-time", this)
        {
            DisplayName = "Request Processing Time",
            DisplayUnits = "ms"
        };
    }

    [Event(1)]
    public void TokenRequested(string message, long elapsedMilliseconds)
    {
        WriteEvent(1, message, elapsedMilliseconds);
        _requestCounter?.WriteMetric(elapsedMilliseconds);
    }

    protected override void Dispose(bool disposing)
    {
        _requestCounter?.Dispose();
        _requestCounter = null;

        base.Dispose(disposing);
    }
}
