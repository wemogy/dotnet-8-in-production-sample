using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Demo.Api;

public static class Observability
{
    public static readonly string ServiceName = typeof(Observability).Assembly.GetName().Name!;

    public static readonly string ServiceVersion = typeof(Observability).Assembly.GetName().Version != null
        ? typeof(Observability).Assembly.GetName().Version!.ToString()
        : "0.0.0";

    // Define a default ActivitySource
    public static readonly ActivitySource Default = new ActivitySource(ServiceName);

    // Define a default Meter with name and version
    public static readonly Meter Meter = new(ServiceName, ServiceVersion);

    // Create Counters, Histograms, etc. from that default Meter
    public static readonly Counter<long> Pings = Meter.CreateCounter<long>("service_countername", description: "Total number of pings");
    public static readonly Histogram<int> PingDelay = Meter.CreateHistogram<int>("service_histgramname", "ms", "Think time in ms for a ping");

    public static void ConfigureStandardLogger(this ILoggingBuilder builder)
    {
        builder.ClearProviders();
        builder.AddConsole();
    }
}
