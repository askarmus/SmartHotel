using MassTransit;
using BookingService.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BookingCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]);
            host.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        cfg.ReceiveEndpoint("order-queue", e =>
        {
            e.ConfigureConsumer<BookingCreatedConsumer>(context);
        });
    });

});
var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();