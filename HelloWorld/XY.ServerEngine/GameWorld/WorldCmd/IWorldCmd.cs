using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    public interface IWorldCmd
    {
        int GetCmdKey();

        void Execute(string message,Session session,IGame game);

        

    }
}
