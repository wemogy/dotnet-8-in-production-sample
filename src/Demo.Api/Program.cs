using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Swagger
var x = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Demo API", Version = "v1" });

    // Include XML comments to Swagger documentation
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, x));

    // Remove trailing "Dto" from schema names
    c.CustomSchemaIds(x => x.Name[..x.Name.LastIndexOf("Dto")]);

    // Add Operation ID based on controller method name and remove trailing "Async"
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}"[..$"{e.ActionDescriptor.RouteValues["action"]}".LastIndexOf("Async")]);
});

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

app.Run();
