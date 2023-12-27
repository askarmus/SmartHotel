
using SmartHotel.BookingService.Consumers;
using SmartHotel.BookingService.CQRS.Commands.CreateBooking;
using SmartHotel.BookingService.Data;
using SmartHotel.BookingService.Repository;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHotel.Infrastructure.AuthenticationManager;
using SmartHotel.Infrastructure.Behaviours;
using SmartHotel.Infrastructure.Exceptions;
using SmartHotel.Infrastructure.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddCustomJwtAuthentication(builder.Configuration["Jwt:Secret"], builder.Configuration["Jwt:Issuer"]);
builder.Host.UseSerilogLogger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
//builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();

builder.Services.AddDbContext<BookingDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BookingStatusUpdateConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        cfg.ReceiveEndpoint("booking-queue", e =>
        {
            e.ConfigureConsumer<BookingStatusUpdateConsumer>(context);
        });
    });
});


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