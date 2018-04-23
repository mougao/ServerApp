using SuperSocket.SocketBase.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerApp
{
    public class MSCommandFilterAttribute : CommandFilterAttribute
    {
        public override void OnCommandExecuted(SuperSocket.SocketBase.CommandExecutingContext commandContext)
        {
            var key = commandContext.RequestInfo.Key;
            //if (key != MsgCode.LG_CTL_LOGIN.ToString("D") &&
            //    key != MsgCode.LG_CTL_MODIFYPSW.ToString("D") && 
            //    key != MsgCode.LG_CTL_REGIST.ToString("D") &&
            //    key != MsgCode.LG_RG_KEY_REQ.ToString("D") &&
            //    key != MsgCode.LG_CTL_SERVERLIST.ToString("D")&&
            //    key != MsgCode.SERVER_GTL_VALIDATE.ToString("D")&&
            //    key != MsgCode.SERVER_GTL_REGIST.ToString("D")&&
            //    key != MsgCode.SERVER_GTL_GAMESTATUS.ToString("D"))
            //{
            //    commandContext.Cancel = true;
            //}         
        }

        public override void OnCommandExecuting(SuperSocket.SocketBase.CommandExecutingContext commandContext)
        {

        }
    }
}
