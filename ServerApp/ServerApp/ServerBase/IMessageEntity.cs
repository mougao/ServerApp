using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerApp
{
    public interface IMessageEntity
    {
        int Serialize(byte[] data,int offset);
    }
}
