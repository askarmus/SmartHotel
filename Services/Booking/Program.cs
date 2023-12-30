
using SmartHotel.BookingService.Consumers;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using SmartHotel.BookingService.Data;
using SmartHotel.BookingService.Repository;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.AuthenticationManager;
using SmartHotel.Infrastructure.Exceptions;
using SmartHotel.Infrastructure.Logging;
using System.Reflection;
using SmartHotel.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using SmartHotel.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddCustomJwtAuthentication(builder.Configuration["Jwt:Secret"], builder.Configuration["Jwt:Issuer"]);
builder.Host.UseSerilogLogger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();

builder.Services.AddDbContext<BookingDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.ConfigureMassTransit<BookingStatusUpdateConsumer>(builder.Configuration);

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