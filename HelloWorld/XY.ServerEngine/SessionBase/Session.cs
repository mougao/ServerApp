using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XY.CommonBase;
using XY.MessageEntity;

namespace XY.ServerEngine
{
    /// <summary>
    /// TODO::有效session认证 指定时间内进行报文信息认证，超时连接主动断开
    /// </summary>
    public class Session
    {
        public void Init(BufferManager buffermanager, Socket socket, IServerEngine serverengine)
        {
            _ReadAsyncEventArgs = new SocketAsyncEventArgs();
            _ReadAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _ReadAsyncEventArgs.UserToken = this;

            buffermanager.SetBuffer(_ReadAsyncEventArgs);

            _WriteAsyncEventArgs = new SocketAsyncEventArgs();
            _WriteAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
            _WriteAsyncEventArgs.UserToken = this;

            _Id = Guid.NewGuid().ToString();
            _Socket = socket;
            _CurServerEngine = serverengine;

            LogHelper.Info("创建新的连接 SessionId:"+ _Id);

            _CurServerEngine.AddNewSession(this);

            bool willReadRaiseEvent = _Socket.ReceiveAsync(_ReadAsyncEventArgs);

            if (!willReadRaiseEvent)
            {
                ProcessReceive(_ReadAsyncEventArgs);
            }
        }

        private void Clear(BufferManager buffermanager)
        {
            buffermanager.FreeBuffer(_ReadAsyncEventArgs);
        }

        private void IO_Completed(object sender, SocketAsyncEventArgs e)
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
        private void Receive(SocketAsyncEventArgs e)
        {
            bool willRaiseEvent = _Socket.ReceiveAsync(e);

            if (!willRaiseEvent)
            {
                ProcessReceive(e);
            }
        }

        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                //string recvStr = Encoding.ASCII.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                Console.WriteLine("收到信息内容 ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());

                byte[] sendbuffer = ProcessReceiveWork(e.Buffer, e.Offset, e.BytesTransferred);

                if (sendbuffer == null)
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
                _CurServerEngine.RemoveSession(this);
                CloseSession();
            }
        }

        private byte[] ProcessReceiveWork(byte[] buffer, int offset, int count)
        {
            byte[] ret = null;

            PushLocalBuffer(buffer, offset, count);

            return ret;
        }

        private void PushLocalBuffer(byte[] buffer, int offset, int count)
        {
            if ((_ReceiveOffset + count) > _ReceiveBuffer.Length)
            {
                throw new Exception("客户端信息数据超出当前接收缓存大小！");
            }

            Array.Copy(buffer, offset, _ReceiveBuffer, _ReceiveOffset, count);

            _ReceiveOffset += count;

            Console.WriteLine("添加数据缓存 当前缓存长度：{0}", _ReceiveOffset);

            List<byte[]> messagebuffers = MessageCoder.MessageDecoding(ref _ReceiveBuffer, ref _ReceiveOffset);

            foreach (var mm in messagebuffers)
            {
                MemoryStream ms = new MemoryStream(mm, 0, mm.Length);

                _CurServerEngine.AddReceiveCommand(ms);

                //CMD_LG_CTL_REGIST mss2 = MessageTransformation.Deserialize<CMD_LG_CTL_REGIST>(ms);

                //Console.WriteLine("account:{0},code:{1},psd:{2}", mss2.account, mss2.code, mss2.psw);
            }
        }

        /// <summary>
        /// 发送报文信息
        /// </summary>
        /// <param name="e"></param>
        public bool Send(Byte[] buff, Int32 offset, Int32 count)
        {
            bool ret = false;

            if (_Socket == null)
            {
                Console.WriteLine("发送信息失败！连接已经断开");
                return ret;
            }

            _WriteAsyncEventArgs.SetBuffer(buff, offset, count);

            bool willRaiseEvent = _Socket.SendAsync(_WriteAsyncEventArgs);
            if (!willRaiseEvent)
            {
                ProcessSend(_WriteAsyncEventArgs);
            }

            ret = true;
            return ret;
        }

        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Console.WriteLine("发送数据成功！ ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());
                Receive(e);
            }
            else
            {
                _CurServerEngine.RemoveSession(this);
                CloseSession();

            }
        }

        public void CloseSession()
        {
            try
            {
                Console.WriteLine("关闭连接！ ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());
                _Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception)
            {
                //TODO::关闭连接异常
            }

            _Socket.Close();
            _Socket = null;
        }

        /// <summary>
        /// 是否为无效连接
        /// </summary>
        /// <returns></returns>
        public bool IsInvalidSession()
        {
            bool ret = false;

            if (_Socket == null)
            {
                ret = true;
            }
            

            return ret;
        }

        private string _Id;

        private Socket _Socket = null;
        private IServerEngine _CurServerEngine;

        private SocketAsyncEventArgs _ReadAsyncEventArgs = null;
        private SocketAsyncEventArgs _WriteAsyncEventArgs = null;

        private byte[] _ReceiveBuffer = new byte[2048 * 2];
        private int _ReceiveOffset = 0;

        public string Id { get => _Id; set => _Id = value; }
    }
}
