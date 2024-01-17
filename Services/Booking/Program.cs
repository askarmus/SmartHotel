
using SmartHotel.BookingService.Consumers;
using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.AuthenticationManager;
using SmartHotel.Infrastructure.Exceptions;
using SmartHotel.Infrastructure.Logging;
using SmartHotel.Infrastructure.Services;
using SmartHotel.Infrastructure.Config;
using SmartHotel.Infrastructure.Behaviors;
using Persistance.Repository;
using SmartHotel.BookingService.Persistance;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using SmartHotel.BookingService.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services
    .AddApiVersioning()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddCustomJwtAuthentication(builder.Configuration["Jwt:Secret"], builder.Configuration["Jwt:Issuer"]);
builder.Host.UseSerilogLogger();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();
builder.Services.AddDbContext<BookingDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.ConfigureMassTransit<BookingStatusUpdateConsumer>(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();

var app = builder.Build();

app.UseMiddleware<ExceptionLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}
app.UseAuthorization();
app.MapControllers();
app.Run();