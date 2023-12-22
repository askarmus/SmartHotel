
using BookingService.Consumers;
using BookingService.Data;
using BookingService.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Infrastructure.AuthenticationManager;
using SmartTicket.Infrastructure.Logging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Host.UseSerilogLogger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<BookingDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });
    });
    x.AddConsumer<BookingStatusUpdateConsumer>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();