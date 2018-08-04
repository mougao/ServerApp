using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    public enum WorkItemType
    {
        Login,
        Logout,
    }


    public interface IWorkItem
    {
        void DoWork(GameWorld world);

    }
}
