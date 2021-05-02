using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuCore.Input.Core;
using MuCore.Queue;

namespace MuCore.Input.Infrastructure
{
    internal class ClientInputQueue
    {
        private MuQueue<ClientInputEntity> Queue { get; } = new MuQueue<ClientInputEntity>();

        public void EnqueueClientInput(string input, Guid clientId)
        {
            Queue.Enqueue(new ClientInputEntity(input, clientId));
        }

        public async Task<ClientInputEntity> DequeueClientInput()
        {
            return await Queue.Next();
        }
    }
}
