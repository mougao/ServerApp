using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    /// <summary>
    /// 模板创建器（更具不同的状态值生成不同的模板实体）
    /// </summary>
    public class WorldTemplateCreater
    {

        public static BattleActor GetBattleActor()
        {
            BattleActor ret = new BattleActor();

            ret.Hp = BattleActorTemplate.GetHP();
            ret.Mp = BattleActorTemplate.GetMP();
            ret.Att = BattleActorTemplate.GetATT();
            ret.Def = BattleActorTemplate.GetDEF();
            ret.Speed = BattleActorTemplate.GetSpeed();

            return ret;
        }

        
    }
}
