using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XY.ServerEngine
{
    public interface IServerEngine
    {
        bool AddReceiveCommand(MemoryStream messagestream);

        bool AddReceiveCommand(IWorkItem workitem);
    }
}
