using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using XY.CommonBase;
using XY.MessageEntity;

namespace XYClientTest
{
    class Program
    {
        static void CreatOneConnect()
        {
            try
            {
                int port = 2012;
                string host = "127.0.0.1";
                //string host = "134.175.17.67";
                
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndPoint实例
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket
                Console.WriteLine("Conneting...");
                c.Connect(ipe);//连接到服务器


                CMD_LG_CTL_REGIST rep = new CMD_LG_CTL_REGIST();
                rep.account = "root";
                rep.code = "bsadsdfasf";
                rep.psw = "123456";

                MemoryStream ms = MessageTransformation.Serialize<CMD_LG_CTL_REGIST>(rep);

                byte[] entityData = ms.ToArray();

                byte[] messagedate = MessageCoder.MessageEncoding(entityData);

                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);

                Console.Write("断开连接");
                //c.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        static void Main(string[] args)
        {

            CreatOneConnect();
            CreatOneConnect();


            Console.Write("客户端启动！");
            Console.Read();

        }

    }
}
