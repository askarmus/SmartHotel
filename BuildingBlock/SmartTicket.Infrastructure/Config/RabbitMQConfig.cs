using Microsoft.Extensions.DependencyInjection;

namespace SmartHotel.Infrastructure.Config;

public static class RabbitMQConfig
{
    public static void ConfigureMassTransit<TConsumer>(this IServiceCollection services, ConfigurationManager configurationManager) where TConsumer : class, IConsumer
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<TConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configurationManager["RabbitMQ:Host"], host =>
                {
                    host.Username(configurationManager["RabbitMQ:UserName"]);
                    host.Password(configurationManager["RabbitMQ:Password"]);
                });
                cfg.ReceiveEndpoint($"queue-{typeof(TConsumer).Name}", e =>
                {
                    e.ConfigureConsumer<TConsumer>(context);
                });
            });
        });
    }




}
