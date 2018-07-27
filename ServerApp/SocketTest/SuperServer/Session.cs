using PaoEntity;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public class Session
    {
        public void Init(BufferManager buffermanager, Socket socket)
        {
            _ReadAsyncEventArgs = new SocketAsyncEventArgs();
            _ReadAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _ReadAsyncEventArgs.UserToken = this;

            buffermanager.SetBuffer(_ReadAsyncEventArgs);

            _WriteAsyncEventArgs = new SocketAsyncEventArgs();
            _WriteAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _WriteAsyncEventArgs.UserToken = this;

            _Socket = socket;

            bool willReadRaiseEvent = _Socket.ReceiveAsync(_ReadAsyncEventArgs);

            if (!willReadRaiseEvent)
            {
                ProcessReceive(_ReadAsyncEventArgs);
            }
        }

        public void Clear(BufferManager buffermanager)
        {
            buffermanager.FreeBuffer(_ReadAsyncEventArgs);
        }

        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }

        }

        /// <summary>
        /// 开始收取报文信息
        /// </summary>
        /// <param name="e"></param>
        public void Receive(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _Socket.ReceiveAsync(e);

            if (!willRaiseEvent)
            {
                ProcessReceive(e);
            }
        }

        public void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //string recvStr = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                Console.WriteLine("收到信息内容");

                byte[] sendbuffer = ProcessReceiveWork(e.Buffer, e.Offset, e.BytesTransferred);

                if(sendbuffer == null)
                {
                    Receive(e);
                }
                else
                {
                    Send(sendbuffer, 0, sendbuffer.Length);
                }
            }
            else
            {
                CloseSession();
            }
        }

        public byte[] ProcessReceiveWork(byte[] buffer, int offset, int count)
        {
            byte[] ret = null;

            MemoryStream ms = new MemoryStream(buffer, offset, count);

            CMD_LG_CTL_REGIST mss2 = Serializer.Deserialize<CMD_LG_CTL_REGIST>(ms);

            MemoryStream wms = new MemoryStream();
            mss2.psw = "1234567890";

            Serializer.Serialize<CMD_LG_CTL_REGIST>(wms, mss2);

            ret = wms.ToArray();

            return ret;
        }

        /// <summary>
        /// 发送报文信息
        /// </summary>
        /// <param name="e"></param>
        public void Send(Byte[] buff, Int32 offset, Int32 count)
        {
            _WriteAsyncEventArgs.SetBuffer(buff, offset, count);

            bool willRaiseEvent = _Socket.SendAsync(_WriteAsyncEventArgs);
            if (!willRaiseEvent)
            {
                ProcessSend(_WriteAsyncEventArgs);
            }
        }

        public void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Console.WriteLine("发送数据！");
                Receive(e);
            }
            else
            {
                CloseSession();
                Console.WriteLine("关闭连接！");
            }
        }

        public void CloseSession()
        {
            try
            {
                _Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                //TODO::关闭连接异常
            }

            _Socket.Close();
        }
        
        private Socket _Socket = null;

        private SocketAsyncEventArgs _ReadAsyncEventArgs = null;
        private SocketAsyncEventArgs _WriteAsyncEventArgs = null;

    }
}
