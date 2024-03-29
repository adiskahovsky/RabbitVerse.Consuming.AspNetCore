namespace RabbitVerse.Consuming.AspNetCore
{
    internal class ConsumingOptions
    {
        public ConsumingOptions(bool recreate)
        {
            Recreate = recreate;
        }
        
        public bool Recreate { get; }
    }
}