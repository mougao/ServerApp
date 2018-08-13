using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 创建新对象
    /// </summary>
    public class WorldCreateNewActor : IWorldCmd
    {
        public void Execute(string message, Session session,IGame game)
        {
            throw new NotImplementedException();
        }

        public int GetCmdKey()
        {
            throw new NotImplementedException();
        }
    }
}
