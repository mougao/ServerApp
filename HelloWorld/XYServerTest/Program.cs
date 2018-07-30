using System;
using XY.ServerEngine;

namespace XYServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpServer server = new TcpServer("127.0.0.1", 2012);

            server.Start();

            Console.Read();
        }
    }
}
