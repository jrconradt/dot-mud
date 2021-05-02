using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuCore.Queue
{
    public class QueueService
    {
        private ConcurrentDictionary<string, MuQueueEntity> Queues { get; set; }

        public void Enqueue(MuQueueEntity entity)
        {

        }
    }
}
