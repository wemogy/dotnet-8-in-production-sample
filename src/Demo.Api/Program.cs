using System.Reflection;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Demo.Api;
using Demo.Api.HealthChecks;
using Demo.Api.Repositories;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Wemogy.CQRS;
using Observability = Demo.Api.Observability;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(c => c.ConfigureStandardLogger());

var logger = LoggerFactory.Create(logger => logger.ConfigureStandardLogger()).CreateLogger<Program>();
logger.LogInformation("Demo Information");
logger.LogWarning("Demo Warning");
logger.LogError("Demo Error");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<TodoRepository>();

// Swagger
var x = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Demo API", Version = "v1" });

    // Include XML comments to Swagger documentation
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, x));

    // Remove trailing "Dto" from schema names if present
    c.CustomSchemaIds(x => x.Name.EndsWith("Dto") ? x.Name[..x.Name.LastIndexOf("Dto")] : x.Name);

    // Add Operation ID based on controller method name and remove trailing "Async" if present
    c.CustomOperationIds(e => e.ActionDescriptor.RouteValues["action"].EndsWith("Async")
        ? $"{e.ActionDescriptor.RouteValues["action"]}"[..$"{e.ActionDescriptor.RouteValues["action"]}".LastIndexOf("Async")]
        : e.ActionDescriptor.RouteValues["action"]);
});

// Skip rest of setup, if app is executed in context of dotnet-swagger
if (Assembly.GetEntryAssembly()?.FullName?.Contains("dotnet-swagger") == true)
{
    return;
}

// Metrics
builder.Services.AddOpenTelemetry().WithMetrics(builder =>
{
    builder.AddMeter(Observability.Meter.Name);

    builder.AddRuntimeInstrumentation();
    builder.AddHttpClientInstrumentation();
    builder.AddAspNetCoreInstrumentation();

    builder.AddPrometheusExporter();
});

// Traces
builder.Services.AddOpenTelemetry().WithTracing(builder =>
{
    builder.AddSource(Observability.ServiceName);
    builder.ConfigureResource((resource) =>
    {
        resource.AddService(Observability.ServiceName, Observability.ServiceVersion);
    });

    builder.AddAspNetCoreInstrumentation();
    builder.AddEntityFrameworkCoreInstrumentation();

    // builder.AddOtlpExporter(oltpOptions =>
    // {
    //     oltpOptions.Endpoint = new Uri("<JAEGER_ENDPOINT>");
    // });
});

// Azure
// builder.Services.AddOpenTelemetry().UseAzureMonitor(azureMonitorOptions =>
// {
//     azureMonitorOptions.ConnectionString = "<APP_INSIGHTS_CONNECTION_STRING>";
//     azureMonitorOptions.SamplingRatio = 1f;
// });

// Setup CQRS
builder.Services.AddCQRS();

// Setup HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck<MyCustomHealthCheck>("MyCustomHealthCheck");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add health checks endpoint
app.MapHealthChecks("/healthz");

app.Run();
