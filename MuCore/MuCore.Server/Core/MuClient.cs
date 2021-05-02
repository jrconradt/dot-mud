using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MuCore.Server.Core
{
    public class MuClient
    {
        public Guid ClientId { get; } = Guid.NewGuid();
        private TcpClient Client { get; set; }

        public MuClient(TcpClient client)
        {
            Client = client;
        }

        public bool HasData => Client.Available != default(int);

        public async Task<string> GetLine()
        {
            var bytes = Client.Available;
            if (bytes > HostConfig.ReadMax) Client.Client.Close();
            byte[] buffer = new byte[bytes];
            await Client.Client.ReceiveAsync(buffer, SocketFlags.None);
            return buffer.ToString();
        }

        public async Task Send(string output)
        {
            using (var stream = Client.GetStream())
            {
                if (!stream.CanWrite) return;
                var byteOutput = Encoding.ASCII.GetBytes(output);
                await stream.WriteAsync(byteOutput);
            }
        }
    }
}
