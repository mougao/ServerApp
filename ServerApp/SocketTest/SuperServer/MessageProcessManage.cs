using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer
{
    /// <summary>
    /// 生产者消费者管理器
    /// </summary>
    public class MessageProcessManage
    {
        private ConcurrentQueue<ProcessItem> _Queue = new ConcurrentQueue<ProcessItem>();

        public void InitProcessManage()
        {
            
        }

        public void AddTail(ProcessItem item)
        {
            if(item!=null)
            {
                _Queue.Enqueue(item);
            }
        }

        public ProcessItem RemoveHead()
        {
            ProcessItem ret = null;

            if(!_Queue.TryDequeue(out ret))
            {
                ret = null;
            }

            return ret;
        }


    }
}
