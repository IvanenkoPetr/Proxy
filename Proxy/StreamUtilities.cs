using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    public static class StreamUtilities
    {
        public static async Task DoComunicationWithServerAsync(ServerInfo speechServerInfo, NetworkStream clientRequest)
        {
            using (var TcpClientForServer = new TcpClient())
            {
                TcpClientForServer.Connect(ServerInfo.LocalHostIP, speechServerInfo.Port);
                using (var serverStream = TcpClientForServer.GetStream())
                {
                    await clientRequest.CopyToAsync(serverStream);
                    await serverStream.CopyToAsync(clientRequest);
                }
            }
        }

    }

}

