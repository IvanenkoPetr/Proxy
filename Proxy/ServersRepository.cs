using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxy
{

    class ServersRepository
    {
        public static ServersRepository Repository => repository.Value;

        private object lockObject = new object();

        private readonly static Lazy<ServersRepository> repository = new Lazy<ServersRepository>(
            delegate {

                var serversFromConfig = new ConfigReader().ServerPorts;
                return new ServersRepository(serversFromConfig.Select(a => new ServerInfo(a))); 
            }
        );

  

        private readonly ServerInfo[] servers;

        private ServersRepository(IEnumerable<ServerInfo> servers)
        {
            this.servers = servers.ToArray();
        }

        public ServerInfo GetLeastBusyServer()
        {
            lock (lockObject)
            {
                var lessLoadedServer = servers.OrderBy(a => a.NumberOfConnections).First();
                lessLoadedServer.NumberOfConnections++;
                return lessLoadedServer;
            }            
        }

        public void ReleaseServer(ServerInfo server)
        {
            lock (lockObject)
            {   
                server.NumberOfConnections--;
            }
        }


    }
}
