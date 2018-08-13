using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 通用报文
    /// </summary>
    public class WorldNormalCmd : IWorldCmd
    {
        public void Execute(string message, Session session, IGame game)
        {
            
        }

        public int GetCmdKey()
        {
            return 0x10000001;
        }
    }
}
