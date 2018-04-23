using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public enum SessionType
    {
        Client,
        Gateway,
    }

    public class PlayerSession : AppSession<PlayerSession, MSRequestInfo>
    {
        public int UserId { get; internal set; }

        public int GameHallId { get; internal set; }

        public int RoomId { get; internal set; }

        protected override void OnSessionStarted()
        {
            this.Send("Welcome to SuperSocket Game Server");
        }

        protected override void HandleUnknownRequest(MSRequestInfo requestInfo)
        {
            this.Send("Unknow request");
        }

        protected override void HandleException(Exception e)
        {
            this.Send("Application error: {0}", e.Message);
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add you logics which will be executed after the session is closed
            base.OnSessionClosed(reason);
        }

        public void Send(IMessageEntity entity)
        {
            Send(m_Encoder.EncodeMessage(entity));
        }

        private IMessageEncoder m_Encoder;
        public void Initialize(IMessageEncoder encoder)
        {
            m_Encoder = encoder;
        }

        public SessionType SessionType { get; set; }
    }
}
