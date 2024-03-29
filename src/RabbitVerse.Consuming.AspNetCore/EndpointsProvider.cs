using System.Collections.Generic;
using RabbitVerse.Consuming.Contract;
using RabbitVerse.Consuming.DTO;

namespace RabbitVerse.Consuming.AspNetCore
{
    internal class EndpointsProvider : IEndpointsProvider
    {
        public HashSet<Endpoint> GetEndpoinds()
        {
            return EndpointsHelper.Get();
        }
    }
}