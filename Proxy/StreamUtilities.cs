using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Proxy
{
    public static class StreamUtilities
    {

        public static byte[] ReadStream(NetworkStream stream)
        {
            
            IEnumerable<byte> result = new List<byte>();
            if (stream.CanRead)
            {
                byte[] readBuffer = new byte[1024];
                do
                {                    
                    var numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);                   
                    result = result.Concat(readBuffer.Take(numberOfBytesRead));
                }
                while (stream.DataAvailable);
            }
            return result.ToArray();

        }

        public static byte[] GetServerResponse(int server, byte[] clientRequest)
        {
            TcpClient TcpClientForServer = new TcpClient();
            TcpClientForServer.Connect("127.0.0.1", server);

            var serverStream = TcpClientForServer.GetStream();
            serverStream.Write(clientRequest, 0, clientRequest.Length);

            var responseFromServer = ReadStream(serverStream);

            serverStream.Close();
            TcpClientForServer.Close();

            return responseFromServer;
            //return new byte[1];


        }

    }
}
