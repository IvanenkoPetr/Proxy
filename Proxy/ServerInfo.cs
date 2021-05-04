using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    public class ServerInfo
    {
        public const string LocalHostIP = "127.0.0.1";
        public int Port { get; }
        public int NumberOfConnections { get; set; }

        public ServerInfo(int port)
        {
            Port = port;
        }
    }
}
