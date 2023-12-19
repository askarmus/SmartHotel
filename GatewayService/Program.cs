using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using static SmartTicket.Infrastructure.AuthenticationManager.CustomJwtAuthExtension;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddCustomJwtAuthentication(builder.Configuration["Jwt:Secret"], builder.Configuration["Jwt:Issuer"]);

var app = builder.Build();

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();
app.Run();