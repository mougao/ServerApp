using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public class ServerManager<TSession>
        where TSession: ISessionItem
    {
        public void Init(IConnectCreater creater, IMessagePacketReceiver receiver, IMessagePacketSender sender)
        {
            _CurConnectCreater = creater;
            _CurReceiver = receiver;
            _CurSender = sender;
            _CurSessionItems = new List<TSession>();
        }

        public void Run()
        {

        }

        public void Stop()
        {

        }

        //网络连接创建者 （监听或者发起连接）
        private IConnectCreater _CurConnectCreater;
        //网络建立的连接对象实体集合
        private List<TSession> _CurSessionItems;
        //网络报文接收者
        private IMessagePacketReceiver _CurReceiver;
        //网络报文发送者
        private IMessagePacketSender _CurSender;

    }
}
