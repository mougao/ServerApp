using System;
using XY.ServerEngine;

namespace XYServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XYServerEngine server = new XYServerEngine("127.0.0.1", 2012);

            server.Start();

            Console.Read();
        }
    }
}
