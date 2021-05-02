using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MuCore.Input.Infrastructure;

namespace MuCore.Input.Application
{
    public class ClientInputSerice
    {
        private Task InputProcesser { get; set; }

        private bool Running { get; set; } = false;

        private static ClientInputQueue InputQueue { get; } = new ClientInputQueue();
        private InputParser Parser { get; set; }

        public ClientInputSerice(InputParser parser)
        {
            Parser = parser;
        }

        public void Start()
        {
            if (Running) return;

            Running = true;
            InputProcesser = Task.Run(ProcessInput);
        }

        public void Stop()
        {
            if (!Running) return;
            Running = false;

            InputProcesser.Wait();
        }

        public async void ProcessInput()
        {
            while(Running)
            {
                var inputEntity = await InputQueue.DequeueClientInput();
                _ = Parser.Parse(inputEntity);
                Thread.Yield();
            }
        }

        public static void Enqueue(string input, Guid clientId)
        {
            InputQueue.EnqueueClientInput(input, clientId);
        }
    }
}
