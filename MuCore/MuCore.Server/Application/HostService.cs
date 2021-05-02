using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MuCore.Input.Application;
using MuCore.Server.Core;
using MuCore.Server.Infrastructure;

namespace MuCore.Server.Application
{
    public class HostService
    {
        private Task ClientAccepter { get; set; } = null;
        private Task InputAccepter { get; set; } = null;

        private ClientMap Clients { get; } = new ClientMap();
        private TcpListener Listener { get; set; }
        private bool Running { get; set; } = false;

        public HostService()
        {

        }

        public void StartListening()
        {
            if (Running) return;

            Running = true;
            Listener = new TcpListener(HostConfig.LocalAddress, HostConfig.Port);
            ClientAccepter = Task.Run(AcceptConnections);
            InputAccepter = Task.Run(StartListening);
        }

        public void StopListening()
        {
            Running = false;
            Task.WaitAll(new Task[] { ClientAccepter, InputAccepter });
        }

        public async void AcceptConnections()
        {
            Listener.Start();
            while (Running)
            {
                var client = await Listener.AcceptTcpClientAsync();
                Clients.AddClient(new MuClient(client));
                Thread.Yield();
            }
        }

        public void Listen()
        {
            while (Running)
            {
                Parallel.ForEach(Clients.GetReadyClients(), async client =>
                {
                    if (!Running) return;

                    var input = await client.GetLine();
                    ReceiveLine(client.ClientId, input);
                });
                Thread.Yield();
            }
        }

        public void ReceiveLine(Guid clientId, string input)
        {
            ClientInputSerice.Enqueue(input, clientId);
        }

        //Going to need an output queue mechanism to pull from for this at some point
        public async void SendToClient(Guid clientId, string output)
        {
            var client = Clients.GetClient(clientId);
            await client.Send(output);
        }
    }
}
