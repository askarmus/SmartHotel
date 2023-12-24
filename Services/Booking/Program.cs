
using BookingService.Consumers;
using BookingService.Data;
using BookingService.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Infrastructure.AuthenticationManager;
using SmartTicket.Infrastructure.Exceptions;
using SmartTicket.Infrastructure.Logging;
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
        cfg.ReceiveEndpoint("order-queue", e =>
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