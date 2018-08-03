using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsClient
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


        public static string GetCurMessage()
        {
            string ret = "";

            if (_Client != null)
            {
                ret = _Client.strReceiveMessage;

                _Client.strReceiveMessage = "";
            }

            return ret;
        }

    }
}
