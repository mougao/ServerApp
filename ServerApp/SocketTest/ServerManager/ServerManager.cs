using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public class ServerManager
    {
        public void Init(IConnectCreater creater, IMessagePacketReceiver receiver, IMessagePacketSender sender)
        {
            _CurConnectCreater = creater;
            _CurReceiver = receiver;
            _CurSender = sender;
            _CurSessionItems = new List<ISessionItem>();
        }

        public void Run()
        {
            _CurConnectCreater.Run(AddNewSessionItem, RemoveNewSessionItem);
        }

        public void Stop()
        {

        }

        private void AddNewSessionItem(ISessionItem item)
        {
            if(item == null)
            {
                Console.WriteLine("error:AddNewSessionItem item对象为空！");
            }
            else
            {
                item.InitSession(_CurReceiver, _CurSender);
                _CurSessionItems.Add(item);
            }
        }

        private void RemoveNewSessionItem(ISessionItem item)
        {
            _CurSessionItems.Remove(item);
        }

        //网络连接创建者 （监听或者发起连接）
        private IConnectCreater _CurConnectCreater;
        //网络建立的连接对象实体集合
        private List<ISessionItem> _CurSessionItems;
        //网络报文接收者
        private IMessagePacketReceiver _CurReceiver;
        //网络报文发送者
        private IMessagePacketSender _CurSender;

    }
}
