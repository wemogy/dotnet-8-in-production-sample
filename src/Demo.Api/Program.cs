using System.Reflection;
using Demo.Api.HealthChecks;
using Demo.Api.Repositories;
using Wemogy.CQRS;

var builder = WebApplication.CreateBuilder(args);

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
// Setup CQRS
builder.Services.AddCQRS();

// Setup HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck<MyCustomHealthCheck>("MyCustomHealthCheck");

// Swagger

// Skip rest of setup, if app is executed in context of dotnet-swagger
if (Assembly.GetEntryAssembly()?.FullName?.Contains("dotnet-swagger") == true)
{
    return;
}

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
