using System;
using System.Collections.Generic;
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
        public void Init(BufferManager buffermanager, Socket socket, Action<Session> callback)
        {
            _ReadEventArgs = new SocketAsyncEventArgs();
            _ReadEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _ReadEventArgs.UserToken = this;

            buffermanager.SetBuffer(_ReadEventArgs);

            _WriteEventArgs = new SocketAsyncEventArgs();
            _WriteEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _WriteEventArgs.UserToken = this;

            _Socket = socket;
            _CallBack = callback;

            bool willReadRaiseEvent = _Socket.ReceiveAsync(_ReadEventArgs);

            if (!willReadRaiseEvent)
            {
                ProcessReceive(_ReadEventArgs);
            }


        }

        public void Clear(BufferManager buffermanager)
        {
            buffermanager.FreeBuffer(_ReadEventArgs);
            buffermanager.FreeBuffer(_WriteEventArgs);
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
                string recvStr = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                Console.WriteLine("收到信息内容：{0} ", recvStr);

                Send(e.Buffer, e.Offset, e.BytesTransferred);

                Receive(e);
            }
            else
            {
                CloseSession();
            }
        }

        /// <summary>
        /// 发送报文信息
        /// </summary>
        /// <param name="e"></param>
        public void Send(Byte[] buff, Int32 offset, Int32 count)
        {
            _WriteEventArgs.SetBuffer(buff, offset, count);

            bool willRaiseEvent = _Socket.SendAsync(_WriteEventArgs);
            if (!willRaiseEvent)
            {
                ProcessSend(_WriteEventArgs);
            }
        }

        public void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Console.WriteLine("发送数据！");
            }
            else
            {
                CloseSession();
            }
            
        }

        public void CloseSession()
        {
            _CallBack(this);
        }
        
        private Socket _Socket = null;


        private SocketAsyncEventArgs _ReadEventArgs = null;

        private SocketAsyncEventArgs _WriteEventArgs = null;

        private Action<Session> _CallBack = null;
            

        public Socket Socket
        {
            get { return _Socket; }
            set { _Socket = value; }
        }

        public SocketAsyncEventArgs ReadEventArgs
        {
            get { return _ReadEventArgs; }
            set { _ReadEventArgs = value; }
        }

        public SocketAsyncEventArgs WriteEventArgs
        {
            get { return _WriteEventArgs; }
            set { _WriteEventArgs = value; }
        }
    }
}
