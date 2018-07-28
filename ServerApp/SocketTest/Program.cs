using SuperServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //加载服务器配置
            //SuperServerConfig ServerConfig = (SuperServerConfig)System.Configuration.ConfigurationManager.GetSection("SuperServerConfig");

            //IPAddress ip = IPAddress.Parse(ServerConfig.IP);
            //IPEndPoint ipe = new IPEndPoint(ip, ServerConfig.Port);

            //AsyncServer server = new AsyncServer(100, 1024);

            //server.Init();

            //server.Start(ipe);

            TcpServer server = new TcpServer("127.0.0.1", 2012);

            server.Start();

            Console.Read();
        }
    }
}
