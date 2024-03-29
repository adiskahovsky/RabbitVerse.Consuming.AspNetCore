using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RabbitVerse.Consuming.AspNetCore
{
    internal class ConsumingBackground : BackgroundService
    {
        private readonly Consuming _consuming;
        private readonly ConsumingOptions _consumingOptions;

        public ConsumingBackground(Consuming consuming, ConsumingOptions consumingOptions)
        {
            _consuming = consuming;
            _consumingOptions = consumingOptions;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = _consuming.Start(_consumingOptions.Recreate);
            return Task.CompletedTask;
        }
    }
}