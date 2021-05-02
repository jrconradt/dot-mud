using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MuCore.Server.Core;

namespace MuCore.Server.Infrastructure
{
    internal class ClientMap
    {
        private ConcurrentDictionary<Guid,MuClient> Clients { get; } 
            = new ConcurrentDictionary<Guid, MuClient>();

        public ClientMap()
        { }

        public void AddClient(MuClient client) =>
            Clients.TryAdd(client.ClientId, client);

        public void RemoveClient(Guid clientId)
        {
            var client = Clients.FirstOrDefault(pair => pair.Key == clientId);
            Clients.TryRemove(client);
        }

        public IEnumerable<MuClient> GetReadyClients() =>
            Clients.Where(pair => pair.Value.HasData)
                .Select(pair => pair.Value);

        public MuClient GetClient(Guid clientId)
        {
            var found = Clients.TryGetValue(clientId, out var client);
            return client;
        }
    }
}
