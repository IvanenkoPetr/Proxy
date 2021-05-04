using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Proxy
{
    public class ConfigReader
    {
        public IEnumerable<int> ServerPorts { get;}

        public ConfigReader()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json");

            var appConfiguration = builder.Build();
            ServerPorts = appConfiguration.GetSection("Servers").GetChildren().Select(a => int.Parse(a.Value));
        }
    }
}
