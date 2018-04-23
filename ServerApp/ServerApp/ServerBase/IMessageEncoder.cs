using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public interface IMessageEncoder
    {
        ArraySegment<byte> EncodeMessage(IMessageEntity entity);
    }
}
