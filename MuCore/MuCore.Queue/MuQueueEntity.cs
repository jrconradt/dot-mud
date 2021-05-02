using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuCore.Queue
{
    public class MuQueueEntity
    {
        public Guid EntityId { get; } = Guid.NewGuid();
    }
}
