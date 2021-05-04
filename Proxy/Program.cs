using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            await ProxyServerAsync();
        }

        private static async Task ProxyServerAsync()
        {
            var localHostAddres = IPAddress.Parse(ServerInfo.LocalHostIP);
            int port = 1234;

            var proxyTcpServer = new TcpListener(localHostAddres, port);
            proxyTcpServer.Start();

            while (true)
            {
                try
                {     
                    // Подключение клиента
                    var client = await proxyTcpServer.AcceptTcpClientAsync();
                    var clientStream = client.GetStream();

                    // Обмен данными
                    Task.Run(async delegate
                    {
                        try
                        {
                            var speechServerInfo = ServersRepository.Repository.GetLeastBusyServer();
                            await StreamUtilities.DoComunicationWithServerAsync(speechServerInfo, clientStream);
                            ServersRepository.Repository.ReleaseServer(speechServerInfo);

                        }
                        finally
                        {
                            clientStream.Close();
                            client.Close();
                        };
                    });
       
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    proxyTcpServer.Stop();
                    break;
                }

            }
        }
    }
}

