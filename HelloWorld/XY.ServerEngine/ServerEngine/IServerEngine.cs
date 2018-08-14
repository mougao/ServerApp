using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XY.ServerEngine
{
    public interface IServerEngine
    {
        bool AddReceiveCommand(MemoryStream messagestream,Session session);

        bool AddReceiveCommand(IWorkItem workitem);

        void AddEventWordItem(WorkItemType type, Session session);

        void AddNewSession(Session session);

        void RemoveSession(Session session);
    }
}
