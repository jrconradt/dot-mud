using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MuCore.Server.Core
{
    public static class HostConfig
    {
        public static IPAddress LocalAddress { get; } = IPAddress.Parse("127.0.0.1");
        public static Int32 Port = 4565;
        public static uint ReadMax = 10000;
    }
}
