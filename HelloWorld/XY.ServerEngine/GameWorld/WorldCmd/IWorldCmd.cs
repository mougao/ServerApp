using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    public interface IWorldCmd
    {
        int GetCmdKey();

        string Execute(string message,Session session);
    }
}
