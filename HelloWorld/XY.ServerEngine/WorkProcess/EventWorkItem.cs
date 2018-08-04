using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    public class EventWorkItem : IWorkItem
    {
        private Session _Session;
        private WorkItemType _WorkType;

        public EventWorkItem(WorkItemType type,Session session)
        {
            _Session = session;
            _WorkType = type;
        }

        public void DoWork(GameWorld world)
        {
            world.ExecuteEvent(_WorkType, _Session);
        }
    }
}
