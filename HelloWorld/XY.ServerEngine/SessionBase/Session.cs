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

            _Id = CreateSessionId();
            _Socket = socket;
            _CurServerEngine = serverengine;

            _CurServerEngine.AddNewSession(this);

            bool willReadRaiseEvent = _Socket.ReceiveAsync(_ReadAsyncEventArgs);

            if (!willReadRaiseEvent)
            {
                ProcessReceive(_ReadAsyncEventArgs);
            }
        }

        public long CreateSessionId()
        {
            long ret = 0;

            var ran = new Random(Guid.NewGuid().GetHashCode());

            string iii = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));

            long.TryParse(iii, out ret);

            return ret;
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

                //LogHelper.Debug("收到信息内容 ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());

                PushLocalBuffer(e.Buffer, e.Offset, e.BytesTransferred);

                Receive(e);
            }
            else
            {
                
                _CurServerEngine.AddEventWordItem(WorkItemType.Logout, this);

            }
        }

        private void PushLocalBuffer(byte[] buffer, int offset, int count)
        {
            if ((_ReceiveOffset + count) > _ReceiveBuffer.Length)
            {
                LogHelper.Error("客户端信息数据超出当前接收缓存大小！");
                throw new Exception("客户端信息数据超出当前接收缓存大小！");
            }

            Array.Copy(buffer, offset, _ReceiveBuffer, _ReceiveOffset, count);

            _ReceiveOffset += count;

            //LogHelper.Debug("添加数据缓存 当前缓存长度：{0}", _ReceiveOffset);

            List<byte[]> messagebuffers = MessageCoder.MessageDecoding(ref _ReceiveBuffer, ref _ReceiveOffset);

            foreach (var mm in messagebuffers)
            {
                MemoryStream ms = new MemoryStream(mm, 0, mm.Length);

                _CurServerEngine.AddReceiveCommand(ms,this);
            }
        }

        public bool Send(int cmd,string message)
        {
            bool ret = false;

            if (_Socket == null)
            {
                LogHelper.Error("发送信息失败！连接已经断开");
                return ret;
            }

            CMD_BASE_MESSAGE rep = new CMD_BASE_MESSAGE();
            rep.Cmd = cmd;
            rep.Message = message;

            MemoryStream ms = MessageTransformation.Serialize<CMD_BASE_MESSAGE>(rep);

            byte[] entityData = ms.ToArray();

            byte[] messagedate = MessageCoder.MessageEncoding(entityData);

            //_Socket.Send(messagedate);

            SendAsync(messagedate, 0, messagedate.Length);

            ret = true;

            return ret;
        }

        /// <summary>
        /// 发送报文信息
        /// </summary>
        /// <param name="e"></param>
        public bool SendAsync(Byte[] buff, Int32 offset, Int32 count)
        {
            bool ret = false;

            if (_Socket == null)
            {
                LogHelper.Error("发送信息失败！连接已经断开");
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
                LogHelper.Debug("发送数据成功！ ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());
            }
            else
            {
                LogHelper.Debug("发送数据失败！ ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        public void CloseSession()
        {
            try
            {
                _CurServerEngine.RemoveSession(this);
                //LogHelper.Debug("关闭连接！ ThreadId:{0}", Thread.CurrentThread.ManagedThreadId.ToString());
                _Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception ex)
            {
                //关闭连接异常
                LogHelper.Error(ex.Message);
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

        private long _Id;

        private Socket _Socket = null;
        private IServerEngine _CurServerEngine;

        private SocketAsyncEventArgs _ReadAsyncEventArgs = null;
        private SocketAsyncEventArgs _WriteAsyncEventArgs = null;

        private byte[] _ReceiveBuffer = new byte[2048 * 2];
        private int _ReceiveOffset = 0;

        public long Id { get => _Id; set => _Id = value; }
    }
}
