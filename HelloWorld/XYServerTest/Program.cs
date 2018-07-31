using System;
using XY.ServerEngine;

namespace XYServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XYServerEngine server = new XYServerEngine(2012);

            server.Start();

            while(true)
            {
                string cmd = Console.ReadLine();

                if(cmd == "stop")
                {
                    server.Stop();
                    break;
                }
            }

            
        }
    }
}
