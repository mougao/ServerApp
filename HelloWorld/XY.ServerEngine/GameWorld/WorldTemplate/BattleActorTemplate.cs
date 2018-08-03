using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 战斗角色模板
    /// </summary>
    public class BattleActorTemplate
    {
        private readonly static BattleActorTemplate _Template = new BattleActorTemplate();

        private BattleActorTemplate()
        {
            Init();
        }

        public static BattleActorTemplate GetTemplate()
        {
            return _Template;
        }

        public static int GetHP()
        {
            int ret = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());
            ret = r.Next(_Template._MinHp, _Template._MaxHp);

            return ret;
        }

        public static int GetMP()
        {
            int ret = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());
            ret = r.Next(_Template._MinMp, _Template._MaxMp);

            return ret;
        }

        public static int GetATT()
        {
            int ret = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());
            ret = r.Next(_Template._MinAtt, _Template._MaxAtt);

            return ret;
        }

        public static int GetDEF()
        {
            int ret = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());
            ret = r.Next(_Template._MinDef, _Template._MaxDef);

            return ret;
        }

        public static int GetSpeed()
        {
            int ret = 0;

            Random r = new Random(Guid.NewGuid().GetHashCode());
            ret = r.Next(_Template._MinSpeed, _Template._MaxSpeed);

            return ret;
        }

        public void Init()
        {
            _MinHp = 50;
            _MaxHp = 100;

            _MinMp = 10;
            _MaxMp = 20;

            _MinAtt = 5;
            _MaxAtt = 10;

            _MinDef = 2;
            _MaxDef = 5;

            _MinSpeed = 1;
            _MaxSpeed = 2;
        }

        private int _MinHp;
        private int _MaxHp;

        private int _MinMp;
        private int _MaxMp;

        private int _MinAtt;
        private int _MaxAtt;

        private int _MinDef;
        private int _MaxDef;

        private int _MinSpeed;
        private int _MaxSpeed;
    }
}
