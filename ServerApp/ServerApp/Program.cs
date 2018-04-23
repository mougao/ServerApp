using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;

namespace ServerApp
{
    class Program
    {
        private static IBootstrap m_Bootstrap;

        static void Main(string[] args)
        {
            //Console.WriteLine("Redis写入缓存：zhong");

            //RedisCacheHelper.Add("zhong", "zhongzhongzhong", DateTime.Now.AddDays(1));

            //Console.WriteLine("Redis获取缓存：zhong");

            //string str3 = RedisCacheHelper.Get<string>("zhong");

            //Console.WriteLine(str3);

            //Console.WriteLine("Redis获取缓存：nihao");
            //string str = RedisCacheHelper.Get<string>("nihao");
            //Console.WriteLine(str);


            //Console.WriteLine("Redis获取缓存：wei");
            //string str1 = RedisCacheHelper.Get<string>("wei");
            //Console.WriteLine(str1);

            //Console.ReadKey();


            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            if (!StartServer())
            {
                Console.ReadKey();
                return;
            }


            Console.ReadKey();
        }

        static bool StartServer()
        {
            m_Bootstrap = BootstrapFactory.CreateBootstrap();
            if (!m_Bootstrap.Initialize())
            {
                Console.WriteLine("游戏服务器初始化失败!");
                return false;
            }

            if (m_Bootstrap.Start() != StartResult.Success)
            {
                Console.WriteLine("游戏服务器启动失败!");
                return false;
            }
            else
            {
                Console.WriteLine("游戏服务器启动成功!");
                return true;
            }
        }
    }


}
