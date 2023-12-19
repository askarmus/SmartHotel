using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");
app.UseOcelot().Wait();
app.Run();