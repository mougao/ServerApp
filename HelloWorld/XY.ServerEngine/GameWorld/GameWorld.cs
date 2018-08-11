using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace XY.ServerEngine
{
    public class GameWorld
    {
        private Dictionary<int, IWorldCmd> _Cmds = new Dictionary<int, IWorldCmd>();

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
            RegisterCmd();
            Console.Write("启动世界对象");
        }

        public void CreateActor(string ActorId)
        {
            BattleActor actor = WorldTemplateCreater.GetBattleActor();
            actor.ActorId = ActorId;
            _BattleActors.Add(actor.ActorId, actor);
        }

        public void RegisterCmd()
        {

        }

        public void ExecuteCmd(int cmdid,string message, Session sesson)
        {
            Console.WriteLine("收到报文cmd:{0} message:{1} session:{2} threadid:{3}",cmdid,message,sesson.Id,Thread.CurrentThread.ManagedThreadId);

            if(_Cmds.ContainsKey(cmdid))
            {
                _Cmds[cmdid].Execute(message, sesson);
            }

            sesson.Send(cmdid, message);
        }

        public void ExecuteEvent(WorkItemType type, Session sesson)
        {

            if(type == WorkItemType.Login)
            {
                Console.WriteLine("有客户端登陆 session:{0} threadid:{1}", sesson.Id, Thread.CurrentThread.ManagedThreadId);

                sesson.Send(1, "你是谁？");
            }
            else if(type == WorkItemType.Logout)
            {
                Console.WriteLine("有客户端登出 session:{0} threadid:{1}", sesson.Id, Thread.CurrentThread.ManagedThreadId);
            }

            

        }


        private Dictionary<string, BattleActor> _BattleActors = new Dictionary<string, BattleActor>();
    }
}
