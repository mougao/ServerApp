using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer
{
    public class SuperServer
    {
        private TcpServer _CurServer = null;

        private MessageProcessManage _CurProcessManager = new MessageProcessManage();

        private bool IsRun = false;

        public void Init(string strIp, int nPort)
        {
            _CurServer = new TcpServer(strIp, nPort);
        }

        public void Start()
        {
            _CurServer.Start();

            IsRun = true;

            //while(IsRun)
            //{




            //}
        }

        public void Stop()
        {
            IsRun = false;
        }

    }
}
