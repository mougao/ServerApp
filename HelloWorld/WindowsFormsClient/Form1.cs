using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XY.CommonBase;
using XY.MessageEntity;

namespace WindowsFormsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void CreatOneConnect()
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


                CMD_BASE_MESSAGE rep = new CMD_BASE_MESSAGE();
                rep.Cmd = 11;
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

                Console.Write("断开连接");
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

        private void button1_Click(object sender, EventArgs e)
        {
            ClientManage.CreatConnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientManage.SendMessage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientManage.Disconnect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = ClientManage.GetCurMessage();
        }
    }
}
