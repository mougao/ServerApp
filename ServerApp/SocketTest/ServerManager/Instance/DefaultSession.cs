using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class DefaultSession : ISessionItem
    {
        public DefaultSession(Socket socket, Action<ISessionItem> callback)
        {
            _CurSocket = socket;
            _DisconnectCallBack = callback;
        }

        public void InitSession(IMessagePacketReceiver receiver, IMessagePacketSender sender)
        {
            _CurReceiver = receiver;
            _CurSender = sender;
        }





        private IMessagePacketReceiver _CurReceiver =null;

        private IMessagePacketSender _CurSender=null;

        private Action<ISessionItem> _DisconnectCallBack = null;

        private Socket _CurSocket = null;

        public Socket CurSocket { get => _CurSocket; }

    }
}
