using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using XY.CommonBase;
using XY.MessageEntity;

namespace XY.ServerEngine
{
    public class MessageWorkItem : IWorkItem
    {
        private MemoryStream _Ms;
        private Session _Session;

        public MessageWorkItem(MemoryStream ms, Session session)
        {
            _Ms = ms;
            _Session = session;
        }

        public void DoWork(GameWorld world)
        {

            try
            {
                CMD_BASE_MESSAGE mss2 = MessageTransformation.Deserialize<CMD_BASE_MESSAGE>(_Ms);

                world.ExecuteCmd(mss2.Cmd, mss2.Message, _Session);
            }
            catch (Exception ex)
            {
                LogHelper.Error("MessageWorkItem:DoWork error:{0}", ex.Message);
            }
        }


    }
}
