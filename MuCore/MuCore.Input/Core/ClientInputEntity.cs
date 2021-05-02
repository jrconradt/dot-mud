using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MuCore.Queue;

namespace MuCore.Input.Core
{
    public class ClientInputEntity:MuQueueEntity
    {
        public string Input { get; }
        public Guid ClientId { get; }

        public ClientInputEntity(string input, Guid clientId)
        {
            Input = input;
            ClientId = clientId;
        }
    }
}
