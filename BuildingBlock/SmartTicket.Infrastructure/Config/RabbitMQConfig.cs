using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHotel.Infrastructure.Config
{
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
}
