using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public interface ISessionItem
    {
        void InitSession(IMessagePacketReceiver receiver, IMessagePacketSender sender);

        Socket CurSocket {  get; }
    }
}
