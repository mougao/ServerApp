using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYClientTest
{
    public class ClientManage
    {
        private static FormClient _Client = null;

        public static void CreatConnect()
        {
            _Client = new FormClient();

            _Client.Connect();
        }

        public static void Disconnect()
        {
            _Client.CloseConnect();
            _Client = null;
        }

        public static void SendMessage()
        {
            if(_Client!=null)
            {
                _Client.SendTestMessage();
            }
        }


    }
}
