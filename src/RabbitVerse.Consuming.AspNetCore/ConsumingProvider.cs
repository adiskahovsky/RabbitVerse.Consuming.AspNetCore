using Microsoft.Extensions.DependencyInjection;
using RabbitVerse.Consuming.DTO;

namespace RabbitVerse.Consuming.AspNetCore
{
    internal class ConsumingProvider : IConsumingProvider
    {
        private readonly IServiceScope _serviceScope;

        public ConsumingProvider(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
        }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }

        public ConsumerInfo GetConsumerInfo(Endpoint endpoint)
        {
            var consumerInfo = _serviceScope?.ServiceProvider.GetRequiredKeyedService<ConsumerInfo>(ConsumingHelper.GetKey(endpoint.Exchange));

            return consumerInfo;
        }
    }
}