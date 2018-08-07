using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XY.ServerEngine
{
    public class GameWorld
    {
        private string _WorldName = "";
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _WorldName = "yyyyyyy";
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

        public void ExecuteCmd(int cmdid,string message, Session sesson)
        {
            Console.WriteLine("收到报文cmd:{0} message:{1} session:{2} threadid:{3}",cmdid,message,sesson.Id,Thread.CurrentThread.ManagedThreadId);



            sesson.Send(cmdid, message);

        }

        public void ExecuteEvent(WorkItemType type, Session sesson)
        {
            Console.WriteLine("收到event type:{0} session:{1} threadid:{2}", type, sesson.Id, Thread.CurrentThread.ManagedThreadId);
        }


        private Dictionary<string, BattleActor> _BattleActors = new Dictionary<string, BattleActor>();
    }
}
