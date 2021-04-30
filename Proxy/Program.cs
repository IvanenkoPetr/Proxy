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
        static void Main(string[] args)
        {
   
            IPAddress localHostAddres = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            TcpListener server = new TcpListener(localHostAddres, port);       
            server.Start();
  
            while (true)
            {
                try
                {
                    // Подключение клиента
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream clientStream = client.GetStream();

                    // Обмен данными
                    try
                    {

                        var completeClientMessage = StreamUtilities.ReadStream(clientStream);

                        Console.WriteLine(completeClientMessage.Count());

                        Task.Run(delegate
                        {
                            var responseFromServer = StreamUtilities.GetServerResponse(1240, completeClientMessage);
                            clientStream.Write(responseFromServer, 0, responseFromServer.Length);

                            clientStream.Close();
                            client.Close();
                        }
                        );          

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                  
                    }
                }
                catch
                {
                    server.Stop();
                    break;
                }

            }
        }
    }
}
