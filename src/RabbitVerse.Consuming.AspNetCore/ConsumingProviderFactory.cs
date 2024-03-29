
using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitVerse.Consuming.Models.Contract.Provider;

namespace RabbitVerse.Consuming.AspNetCore
{
    internal class ConsumingProviderFactory : IConsumingProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsumingProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConsumingProvider StartScope()
        {
            var scope = _serviceProvider.CreateScope();
            return new ConsumingProvider(scope);
        }
    }
}