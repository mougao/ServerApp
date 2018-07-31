using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using XY.MessageEntity;

namespace XY.ServerEngine
{
    public class DefaultWorkItem : IWorkItem
    {
        private MemoryStream _Ms;

        public DefaultWorkItem(MemoryStream ms)
        {
            _Ms = ms;
        }

        public void DoWork()
        {
            CMD_LG_CTL_REGIST mss2 = MessageTransformation.Deserialize<CMD_LG_CTL_REGIST>(_Ms);

            Console.WriteLine("account:{0},code:{1},psd:{2} ThreadId:{3}", mss2.account, mss2.code, mss2.psw, Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
}
