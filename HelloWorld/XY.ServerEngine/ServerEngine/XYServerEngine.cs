﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using XY.CommonBase;

namespace XY.ServerEngine
{
    class NewMailEventArgs : EventArgs
    {
        private string from;
        private string to;
        private string content;

        public string From { get { return from; } }
        public string To { get { return to; } }
        public string Content { get { return content; } }

        public NewMailEventArgs(string from, string to, string content)
        {
            this.from = from;
            this.to = to;
            this.content = content;
        }
    }

    public class Sender
    {

    }

    public class XYServerEngine : IServerEngine
    {
        protected object SyncRoot = new object();

        private TcpServer _BaseServer = null;

        private GameWorld _GameWorld = new GameWorld();

        private bool _IsStart = false;

        private ConcurrentDictionary<long,Session> _CurSessions = new ConcurrentDictionary<long,Session>();

        private BlockingCollection<IWorkItem> _WorkItems = new BlockingCollection<IWorkItem>();

        private EventHandler<NewMailEventArgs> _newMail;

        private void FaxMsg(object sender, NewMailEventArgs e)
        {
            Console.WriteLine(string.Format("fax receive，from:{0} to:{1} content is:{2}", e.From, e.To, e.Content));
        }


        public XYServerEngine(string strIp, int nPort)
        {
            _BaseServer = new TcpServer(strIp, nPort, this);

            _GameWorld.Init();
        }

        public XYServerEngine( int nPort)
        {
            _BaseServer = new TcpServer(nPort, this);

            _GameWorld.Init();
        }

        public void Start()
        {
            if (_BaseServer.Start())
            {
                _IsStart = true;
                Console.Write("服务器启动成功！\r\n");
            }
            else
            {
                Console.Write("服务器启动失败！\r\n");
                return;
            }

            


            Task.Run(()=> {

                _GameWorld.Start();

                //_newMail += FaxMsg;

                //Sender ss = new Sender();
                //NewMailEventArgs e = new NewMailEventArgs("123", "321", "事件测试");

                //_newMail(ss, e);

                while (_IsStart)
                {
                    for(int i=0;i<100;i++)
                    {
                        IWorkItem item = null;

                        if (_WorkItems.TryTake(out item))
                        {
                            item.DoWork(_GameWorld);
                        }
                    }

                    //IWorkItem item = _WorkItems.Take();

                    //item.DoWork(_GameWorld);
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

        public bool AddReceiveCommand(MemoryStream messagestream,Session session)
        {
            bool ret = false;

            if (messagestream == null)
                return ret;

            MessageWorkItem workitem = new MessageWorkItem(messagestream, session);

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

        public void AddEventWordItem(WorkItemType type,Session session)
        {
            EventWorkItem item = new EventWorkItem(type, session);

            _WorkItems.Add(item);

            LogHelper.Debug("添加新事件事件事件SessionId:" + session.Id);
        }

        public void AddNewSession(Session session)
        {
            try
            {
                if (session != null)
                {
                    if (_CurSessions.TryAdd(session.Id, session))
                    {
                        AddEventWordItem(WorkItemType.Login, session);
                        LogHelper.Debug("添加新的连接SessionId:" + session.Id);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message);
            }
        }

        public void RemoveSession(Session session)
        {
            try
            {
                Session removesession;
                if (_CurSessions.TryRemove(session.Id, out removesession))
                {
                    //AddEventWordItem(WorkItemType.Logout, session);
                    LogHelper.Debug("移除的连接SessionId:" + removesession.Id);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex.Message);
            }

            
        }
    }
}
