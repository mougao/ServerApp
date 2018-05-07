using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    public interface IConnectCreater
    {
        void Run(Action<ISessionItem> addcallback, Action<ISessionItem> removecallback);
    }
}
