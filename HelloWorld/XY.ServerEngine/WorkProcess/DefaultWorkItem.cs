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
            CMD_BASE_MESSAGE mss2 = MessageTransformation.Deserialize<CMD_BASE_MESSAGE>(_Ms);

            Console.WriteLine("cmd:{0},message:{1} ThreadId:{2}", mss2.Cmd, mss2.Message, Thread.CurrentThread.ManagedThreadId.ToString());

            //TODO::报文解析业务实现
        }
    }
}
