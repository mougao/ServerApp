using System;
using System.IO;
using System.Net;
using System.Net.Sockets;



namespace XYClientTest
{
    class Program
    {
        static void CreatOneConnect()
        {
            
            try
            {
                int port = 2012;
                //string host = "127.0.0.1";
                string host = "134.175.17.67";
                
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndPoint实例
                Socket c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket
                Console.WriteLine("Conneting...");
                c.Connect(ipe);//连接到服务器


                CMD_BASE_MESSAGE rep = new CMD_BASE_MESSAGE();
                rep.Cmd = 0x10000001;
                rep.Message = "bsadsdfasf";


                MemoryStream ms = MessageTransformation.Serialize<CMD_BASE_MESSAGE>(rep);

                byte[] entityData = ms.ToArray();

                byte[] messagedate = MessageCoder.MessageEncoding(entityData);

                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);
                c.Send(messagedate, messagedate.Length, 0);

                //Console.Write("断开连接");
                c.Close();
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
            FormClient Client = new FormClient();
            Client.Connect();

            while (true)
            {
                string cmd = Console.ReadLine();

                if(cmd=="q")
                {
                    Client.CloseConnect();
                    break;
                }
                else
                {
                    Client.SendServerMessage(11, cmd);
                }
                
            }

            Console.Write("客户端关闭！");
            Console.Read();

        }

    }
}
