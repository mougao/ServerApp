using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperServer
{
    /// <summary>
    /// 服务器实例
    /// </summary>
    public class SuperServer
    {
        public SuperServer(SuperServerConfig config)
        {
            //if (config == null)
            //    return;
            //_NumConnectedSockets = 0;
            //IPAddress ip = IPAddress.Parse(config.IP);
            //_Ipe = new IPEndPoint(ip, config.Port);
            //_NumConnections = config.NumConnections;
            //_ReceiveBufferSize = config.ReceiveBufferSize;

            //_BufferPool = new BufferManager(_ReceiveBufferSize * _NumConnections * opsToPreAlloc,
            //    _ReceiveBufferSize);

            //_MaxNumberAcceptedClients = new Semaphore(_NumConnections, _NumConnections);
        }

        public SuperServer(string strIp,int nPort)
        {
            IPAddress ip = IPAddress.Parse(strIp);
            _Ipe = new IPEndPoint(ip, nPort);
            _NumConnections = 1000;
            _ReceiveBufferSize = 2048;

            _BufferPool = new BufferManager(_ReceiveBufferSize * _NumConnections * opsToPreAlloc,
                _ReceiveBufferSize);

            _MaxNumberAcceptedClients = new Semaphore(_NumConnections, _NumConnections);
        }
        /// <summary>
        /// 服务器初始化
        /// </summary>
        public void Init()
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

            Console.WriteLine("Press any key to terminate the server process....");
            Console.ReadKey();
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
            Console.WriteLine("Client connection accepted. There are New clients connected to the server");

            Session session = new Session();

            session.Init(_BufferPool, e.AcceptSocket);

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
    }
}
