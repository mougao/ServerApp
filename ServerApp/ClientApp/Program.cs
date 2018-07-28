using Common;
using MsEntity;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {

        static void CreatOneConnect()
        {
            try
            {
                int port = 2012;
                string host = "127.0.0.1";
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndPoint实例
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket
                Console.WriteLine("Conneting...");
                c.Connect(ipe);//连接到服务器


                CMD_LG_CTL_REGIST rep = new CMD_LG_CTL_REGIST();
                rep.account = "root";
                rep.code = "bsadsdfasf";
                rep.psw = "123456";

                MemoryStream ms = new MemoryStream();
                Serializer.Serialize<CMD_LG_CTL_REGIST>(ms, rep);

                byte[] entityData = ms.ToArray();


                byte[] messagedate = MessageCoding.MessageEncoding(entityData);
                

                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);


                
                //for (int i = 0; i < 3; i++)
                //{
                //    string sendStr = "hello!This is a socket test";
                //    byte[] bs = Encoding.ASCII.GetBytes(sendStr);
                //    Console.WriteLine("Send Message");
                //    c.Send(bs, bs.Length, 0);//发送测试信息
                //    string recvStr = "";
                //    byte[] recvBytes = new byte[1024];
                //    int bytes;
                //    bytes = c.Receive(recvBytes, recvBytes.Length, 0);//从服务器端接受返回信息
                //    recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                //    Console.WriteLine("Client Get Message:{0}", recvStr);//显示服务器返回信息
                //}

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
