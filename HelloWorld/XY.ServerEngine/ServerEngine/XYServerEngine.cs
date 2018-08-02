using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using XY.CommonBase;

namespace XY.ServerEngine
{
    public class XYServerEngine : IServerEngine
    {
        protected object SyncRoot = new object();

        private TcpServer _BaseServer = null;

        private bool _IsStart = false;

        private ConcurrentDictionary<string,Session> _CurSessions = new ConcurrentDictionary<string,Session>();

        private BlockingCollection<IWorkItem> _WorkItems = new BlockingCollection<IWorkItem>();

        public XYServerEngine(string strIp, int nPort)
        {

            _BaseServer = new TcpServer(strIp, nPort, this);
        }

        public XYServerEngine( int nPort)
        {
            _BaseServer = new TcpServer(nPort, this);
        }

        public void Start()
        {
            if (_BaseServer.Start())
            {
                _IsStart = true;
                Console.Write("服务器启动成功！");
            }
            else
            {
                Console.Write("服务器启动失败！");
                return;
            }

            Task.Run(()=> {

                while (_IsStart)
                {
                    IWorkItem item = _WorkItems.Take();

                    item.DoWork();
                }

            });

        }

        public void Stop()
        {
            if (!_IsStart)
                return;

            lock (SyncRoot)
            {
                if (!_IsStart)
                    return;

                _IsStart = false;

                //TODO::清空服务器信息
                foreach(var session in _CurSessions.Values)
                {
                    session.CloseSession();
                }

                _CurSessions.Clear();

            }
            
        }

        public bool AddReceiveCommand(MemoryStream messagestream)
        {
            bool ret = false;

            if (messagestream == null)
                return ret;

            DefaultWorkItem workitem = new DefaultWorkItem(messagestream);

            _WorkItems.Add(workitem);

            ret = true;
            return ret;

        }

        public bool AddReceiveCommand(IWorkItem workitem)
        {
            bool ret = false;

            if (workitem == null)
                return ret;

            _WorkItems.Add(workitem);

            ret = true;
            return ret;
        }

        public void AddNewSession(Session session)
        {
            if (session != null)
            {
                if(_CurSessions.TryAdd(session.Id, session))
                {
                    LogHelper.Info("添加新的连接SessionId:"+ session.Id);
                }
            }
        }

        public void RemoveSession(Session session)
        {
            Session removesession;
            if(_CurSessions.TryRemove(session.Id,out removesession))
            {
                LogHelper.Info("移除的连接SessionId:" + removesession.Id);
            }
        }
    }
}
