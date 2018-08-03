using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 战斗角色实例
    /// </summary>
    public class BattleActor
    {
        private string _ActorId;

        private int _Hp;

        private int _Mp;

        private int _Att;

        private int _Def;

        private int _Speed;

        public int Hp { get => _Hp; set => _Hp = value; }
        public int Mp { get => _Mp; set => _Mp = value; }
        public int Att { get => _Att; set => _Att = value; }
        public int Def { get => _Def; set => _Def = value; }
        public int Speed { get => _Speed; set => _Speed = value; }
        public string ActorId { get => _ActorId; set => _ActorId = value; }
    }
}
