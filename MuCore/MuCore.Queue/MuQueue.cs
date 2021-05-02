using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuCore.Queue
{
    public class MuQueue<T> where T:MuQueueEntity
    {
        private ConcurrentQueue<T> Queue { get; } = new ConcurrentQueue<T>();

        public MuQueue() { }

        public void Enqueue(T entity)
        {
            Queue.Enqueue(entity);
        }

        public async Task<T> Next()
        {
            return await Task.Run(() =>
            {
                T entity = default(T);
                while (!Queue.TryDequeue(out entity))
                {
                    if (!Thread.Yield())
                    {
                        Thread.Sleep(100);
                    }
                }
                return entity;
            });
        }
    }
}
