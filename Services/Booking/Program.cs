
using SmartHotel.BookingService.Consumers;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.AuthenticationManager;
using SmartHotel.Infrastructure.Exceptions;
using SmartHotel.Infrastructure.Logging;
using SmartHotel.Infrastructure.Services;
using SmartHotel.Infrastructure.Config;
using SmartHotel.Infrastructure.Behaviors;
using Persistance.Repository;
using SmartHotel.BookingService.Persistance;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();