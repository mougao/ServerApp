using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer
{
    /// <summary>
    /// 单位消费对象
    /// </summary>
    public class ProcessItem
    {
        public ProcessItem(byte[] buffer,int offset,int count, Session session)
        {
            _User = session;
            _MessageStream = new MemoryStream(buffer, offset, count);
        }

        private MemoryStream _MessageStream = null; 

        private Session _User = null;

    }
}
