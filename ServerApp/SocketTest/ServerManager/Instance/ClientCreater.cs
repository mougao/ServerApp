using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class ClientCreater : IConnectCreater
    {
        public void Run(Action<ISessionItem> addcallback, Action<ISessionItem> removecallback)
        {
            throw new NotImplementedException();
        }
    }
}
