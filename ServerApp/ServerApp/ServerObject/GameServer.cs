using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    [MSCommandFilter]
    public class GameServer : AppServer<PlayerSession, MSRequestInfo>
    {
        public GameServer()
           : base(new MSReceiveFilterFactory())
        { }

        private IMessageEncoder MessageEncoder { get; set; }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            MessageEncoder = new DefaultMessageEncoder();

            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        protected override void OnNewSessionConnected(PlayerSession session)
        {
            session.Initialize(MessageEncoder);

            base.OnNewSessionConnected(session);
        }

        protected override void OnSessionClosed(PlayerSession session, CloseReason reason)
        {
            base.OnSessionClosed(session, reason);
        }
    }
}
