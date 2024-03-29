using System;
using System.Collections.Generic;
using RabbitVerse.Consuming.DTO;

namespace RabbitVerse.Consuming.AspNetCore
{
    internal static class EndpointsHelper
    {
        private static readonly HashSet<Endpoint> _endpoints = new HashSet<Endpoint>();

        public static void Add(Endpoint endpoint)
        {
            if(!_endpoints.Add(endpoint))
                throw new ArgumentException("Endpoint already exists");
        }

        public static HashSet<Endpoint> Get() => _endpoints;
    }
}