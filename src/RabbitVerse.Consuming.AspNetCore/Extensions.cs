using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitVerse.Consuming.Contract;
using RabbitVerse.Consuming.DTO;
using RabbitVerse.Consuming.Models.Contract.Provider;

namespace RabbitVerse.Consuming.AspNetCore
{
    public static class Extensions
    {
        public static IServiceCollection AddConsuming(this IServiceCollection services, string serviceName, ConnectionFactory connectionFactory, bool recreate = false)
        {
            services.AddSingleton(sp => new Consuming(
                serviceName,
                connectionFactory,
                sp.GetRequiredService<IConsumingProviderFactory>(),
                sp.GetRequiredService<IEndpointsProvider>()));

            services.AddSingleton<IConsumingProviderFactory, ConsumingProviderFactory>();
            services.AddSingleton<IEndpointsProvider, EndpointsProvider>();
            services.AddSingleton(new ConsumingOptions(recreate));
            services.AddHostedService<ConsumingBackground>();
            return services;
        }

        public static IServiceCollection RegisterConsumer<T>(this IServiceCollection services, string exchange, ServiceLifetime lifetime, int permonentRetryCount, TimeSpan[] onceRetries, TimeSpan? infinityRetry) where T : class, IConsumer
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton<T>();
                    services.AddKeyedSingleton(
                        ConsumingHelper.GetKey(exchange),
                        (sp, _) => new ConsumerInfo(sp.GetRequiredService<T>(), exchange));
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped<T>();
                    services.AddKeyedScoped(
                     ConsumingHelper.GetKey(exchange),
                     (sp, _) => new ConsumerInfo(sp.GetRequiredService<T>(), exchange));
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient<T>();
                    services.AddKeyedTransient(
                     ConsumingHelper.GetKey(exchange),
                     (sp, _) => new ConsumerInfo(sp.GetRequiredService<T>(), exchange));
                    break;
            }

            EndpointsHelper.Add(new Endpoint(exchange, new RetryInfo(permonentRetryCount, infinityRetry, onceRetries)));

            return services;
        }
    }
}