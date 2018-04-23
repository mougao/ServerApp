using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System.Net;
using System.Configuration;

namespace ServerApp
{
    public class MSReceiveFilterFactory : IReceiveFilterFactory<MSRequestInfo>
    {
        public IReceiveFilter<MSRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, IPEndPoint remoteEndPoint)
        {
            return new MSReceiveFilter();
        }
    }
}
