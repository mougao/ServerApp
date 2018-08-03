using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XY.CommonBase;

namespace XY.ServerEngine
{
    public class TcpServer
    {

        public TcpServer(string strIp, int nPort, IServerEngine serverengine = null)
        {
            IPAddress ip = IPAddress.Parse(strIp);
            _Ipe = new IPEndPoint(ip, nPort);
            _NumConnections = 1000;
            _ReceiveBufferSize = 2048;

            _BufferPool = new BufferManager(_ReceiveBufferSize * _NumConnections * opsToPreAlloc,
                _ReceiveBufferSize);

            _MaxNumberAcceptedClients = new Semaphore(_NumConnections, _NumConnections);

            _CurServerEngine = serverengine;
        }

        public TcpServer(int nPort, IServerEngine serverengine = null)
        {
            IPAddress ip = IPAddress.Any;
            _Ipe = new IPEndPoint(ip, nPort);
            _NumConnections = 1000;
            _ReceiveBufferSize = 2048;

            _BufferPool = new BufferManager(_ReceiveBufferSize * _NumConnections * opsToPreAlloc,
                _ReceiveBufferSize);

            _MaxNumberAcceptedClients = new Semaphore(_NumConnections, _NumConnections);

            _CurServerEngine = serverengine;
        }
        /// <summary>
        /// 服务器初始化
        /// </summary>
        private void Init()
        {
            _BufferPool.InitBuffer();
        }
        /// <summary>
        /// 服务器启动
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            Init();

            if (_Ipe == null)
                return false;



            _ListenSocket = new Socket(_Ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _ListenSocket.Bind(_Ipe);
            _ListenSocket.Listen(_Ipe.Port);

            StartAccept(null);

            //Console.WriteLine("开启服务，监听Tcp端口 ThreadId;{0}", Thread.CurrentThread.ManagedThreadId.ToString());
            //Console.ReadKey();
            return true;
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
            Console.WriteLine("Client connection accepted. There are New clients connected to the server ThreadId;{0}", Thread.CurrentThread.ManagedThreadId.ToString());

            Session session = new Session();

            session.Init(_BufferPool, e.AcceptSocket, _CurServerEngine);

            

            _MaxNumberAcceptedClients.Release();

            StartAccept(e);
        }

        const int opsToPreAlloc = 2;
        /// <summary>
        /// Ip信息
        /// </summary>
        private IPEndPoint _Ipe;
        /// <summary>
        /// 最大连接数
        /// </summary>
        private int _NumConnections;
        /// <summary>
        /// 读写缓存大小
        /// </summary>
        private int _ReceiveBufferSize;
        /// <summary>
        /// 缓存池
        /// </summary>
        private BufferManager _BufferPool;
        /// <summary>
        /// 监听Socket
        /// </summary>
        private Socket _ListenSocket;

        /// <summary>
        /// 信号量管理
        /// </summary>
        private Semaphore _MaxNumberAcceptedClients;

        private IServerEngine _CurServerEngine;

    }
}
