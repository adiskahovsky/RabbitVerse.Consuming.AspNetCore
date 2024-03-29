namespace RabbitVerse.Consuming.AspNetCore
{
    internal static class ConsumingHelper
    {
        public static string GetKey(string exchange) => $"consumerInfo:{exchange}";
    }
}