using PaoEntity;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class GameStatusCommand : EntityCommandBase<PlayerSession, CMD_LG_CTL_REGIST>
    {
        protected override void ExcuteEntityCommand(PlayerSession session, CMD_LG_CTL_REGIST entity)
        {
            string error = entity.account;



        }

        public override string Name
        {
            get { return ((uint)MsgCode.LG_RG_KEY_REQ).ToString(); }
        }
    }
}
