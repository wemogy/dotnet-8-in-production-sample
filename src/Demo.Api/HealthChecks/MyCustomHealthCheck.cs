using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Demo.Api.HealthChecks;

public class MyCustomHealthCheck : IHealthCheck
{
    private static int _counter = 0;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_counter++ % 2 == 0)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }

        return Task.FromResult(HealthCheckResult.Unhealthy());
    }
}
