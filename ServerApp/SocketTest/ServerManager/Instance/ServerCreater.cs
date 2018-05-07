using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTest
{
    class ServerCreater : IConnectCreater
    {
        public void Init(string strIp, int nPort,int maxcount)
        {
            IPAddress ip = IPAddress.Parse(strIp);
            _Ipe = new IPEndPoint(ip, nPort);
            _NumConnections = maxcount;

            _MaxNumberAcceptedClients = new Semaphore(_NumConnections, _NumConnections);
        }

        public void Run(Action<ISessionItem> addcallback, Action<ISessionItem> removecallback)
        {
            _AddCallBack = addcallback;
            _RemoveCallBack = removecallback;

            if (_Ipe == null)
            {
                Console.WriteLine("Ip信息未设置！");
                return;
            }

            _ListenSocket = new Socket(_Ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _ListenSocket.Bind(_Ipe);
            _ListenSocket.Listen(_Ipe.Port);

            StartAccept(null);

        }

        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }

            _MaxNumberAcceptedClients.WaitOne();

            bool willRaiseEvent = _ListenSocket.AcceptAsync(acceptEventArg);

            if (!willRaiseEvent)//同步完成
            {
                ProcessAccept(acceptEventArg);
            }
        }

        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            Interlocked.Increment(ref _NumConnectedSockets);
            Console.WriteLine("Client connection accepted. There are {0} clients connected to the server",
                _NumConnectedSockets);

            DefaultSession session = new DefaultSession(e.AcceptSocket,(ss)=> {

                CloseClientSocket(ss);
            });

            _AddCallBack(session);

            StartAccept(e);
        }

        public void CloseClientSocket(ISessionItem session)
        {
            Interlocked.Decrement(ref _NumConnectedSockets);
            _MaxNumberAcceptedClients.Release();

            _RemoveCallBack(session);

            Console.WriteLine("A client has been disconnected from the server. There are {0} clients connected to the server", _NumConnectedSockets);
        }

        /// <summary>
        /// Ip信息
        /// </summary>
        private IPEndPoint _Ipe;
        /// <summary>
        /// 最大连接数
        /// </summary>
        private int _NumConnections;
        /// <summary>
        /// 监听Socket
        /// </summary>
        private Socket _ListenSocket;
        /// <summary>
        /// 当前连接数量
        /// </summary>
        private int _NumConnectedSockets;
        /// <summary>
        /// 信号量管理
        /// </summary>
        private Semaphore _MaxNumberAcceptedClients;

        private Action<ISessionItem> _AddCallBack = null;

        private Action<ISessionItem> _RemoveCallBack = null;
    }
}
