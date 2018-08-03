using System;
using System.Collections.Generic;
using System.Text;

namespace XY.ServerEngine
{
    public class GameWorld
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {

        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            Console.Write("启动世界对象");
        }

        public void CreateActor(string ActorId)
        {
            BattleActor actor = WorldTemplateCreater.GetBattleActor();
            actor.ActorId = ActorId;
            _BattleActors.Add(actor.ActorId, actor);
        }



        private Dictionary<string, BattleActor> _BattleActors = new Dictionary<string, BattleActor>();
    }
}
