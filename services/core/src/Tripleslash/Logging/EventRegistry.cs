using Microsoft.Extensions.Logging;

namespace Tripleslash.Logging;

public static class EventRegistry
{
    public static EventId FailedHttpRequest => new EventId(6500, "FAILED_HTTP");
}