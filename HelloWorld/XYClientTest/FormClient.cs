using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XYClientTest
{
    public class FormClient
    {
        private Socket _socket = null;
        private byte[] buffer = new byte[2048];
        private SocketAsyncEventArgs _ReadAsyncEventArgs = null;

        private byte[] _ReceiveBuffer = new byte[2048 * 2];
        private int _ReceiveOffset = 0;


        private void Receive(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _socket.ReceiveAsync(e);

            if (!willRaiseEvent)
            {
                ProcessReceive(e);
            }
        }

        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }

        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //Console.WriteLine("收到信息内容 ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());

                PushLocalBuffer(e.Buffer, e.Offset, e.BytesTransferred);

                Receive(e);
            }
            else
            {
                CloseConnect();
            }
        }

        private void PushLocalBuffer(byte[] buffer, int offset, int count)
        {
            if ((_ReceiveOffset + count) > _ReceiveBuffer.Length)
            {
                throw new Exception("客户端信息数据超出当前接收缓存大小！");
            }

            Array.Copy(buffer, offset, _ReceiveBuffer, _ReceiveOffset, count);

            _ReceiveOffset += count;

            //Console.WriteLine("添加数据缓存 当前缓存长度：{0}", _ReceiveOffset);

            List<byte[]> messagebuffers = MessageCoder.MessageDecoding(ref _ReceiveBuffer, ref _ReceiveOffset);

            foreach (var mm in messagebuffers)
            {
                MemoryStream ms = new MemoryStream(mm, 0, mm.Length);

                CMD_BASE_MESSAGE mss2 = MessageTransformation.Deserialize<CMD_BASE_MESSAGE>(ms);

                Console.WriteLine("message:{0}",mss2.Message);
            }
        }

        /// <summary>
        /// 发送报文信息
        /// </summary>
        /// <param name="e"></param>
        public bool Send(Byte[] buff)
        {
            bool ret = false;

            if (_socket == null)
            {
                Console.WriteLine("发送信息失败！连接已经断开");
                return ret;
            }

            _socket.Send(buff);

            ret = true;
            return ret;
        }



        public void Connect()
        {
            int port = 2012;
            string host = "127.0.0.1";
            //string host = "134.175.17.67";

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);//把ip和端口转化为IPEndPoint实例
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket
            Console.WriteLine("Conneting...");
            _socket.Connect(ipe);//连接到服务器

            _ReadAsyncEventArgs = new SocketAsyncEventArgs();
            _ReadAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _ReadAsyncEventArgs.UserToken = this;

            _ReadAsyncEventArgs.SetBuffer(buffer, 0, buffer.Length);

            Receive(_ReadAsyncEventArgs);
        }

        public void CloseConnect()
        {
            Console.WriteLine("Close...");
            _socket.Close();
        }

        public void SendTestMessage()
        {
            CMD_BASE_MESSAGE rep = new CMD_BASE_MESSAGE();
            rep.Cmd = 11;
            rep.Message = "bsadsdfasf";

            MemoryStream ms = MessageTransformation.Serialize<CMD_BASE_MESSAGE>(rep);

            byte[] entityData = ms.ToArray();

            byte[] messagedate = MessageCoder.MessageEncoding(entityData);

            _socket.Send(messagedate, messagedate.Length, 0);
        }

        public void SendServerMessage(int cmd,string message)
        {
            CMD_BASE_MESSAGE rep = new CMD_BASE_MESSAGE();
            rep.Cmd = cmd;
            rep.Message = message;

            MemoryStream ms = MessageTransformation.Serialize<CMD_BASE_MESSAGE>(rep);

            byte[] entityData = ms.ToArray();

            byte[] messagedate = MessageCoder.MessageEncoding(entityData);

            _socket.Send(messagedate, messagedate.Length, 0);
        }

        public void ReceiveMessage()
        {

        }

        public void SendMessage()
        {

        }

    }
}
