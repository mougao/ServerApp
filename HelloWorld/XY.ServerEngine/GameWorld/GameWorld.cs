using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using XY.CommonBase;

namespace XY.ServerEngine
{
    public interface IGame
    {
        void AddActor(BattleActor actor);

        void RemoveActor(string actorid);

        BattleActor GetActor(string actorid);
    }

    public class GameWorld: IGame
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
            WorldNormalCmd NCmd = new WorldNormalCmd();

            AddCmd(NCmd);
        }

        private void AddCmd(IWorldCmd cmd )
        {
            if(cmd != null)
            {
                if(_Cmds.ContainsKey(cmd.GetCmdKey()))
                {
                    LogHelper.Error("CmdKey:{0} 重复注册！", cmd.GetCmdKey());
                }
                else
                {
                    _Cmds.Add(cmd.GetCmdKey(), cmd);
                }
            }

        }

        public void ExecuteCmd(int cmdid,string message, Session sesson)
        {
            Console.WriteLine("收到报文cmd:{0} message:{1} session:{2} threadid:{3}",cmdid,message,sesson.Id,Thread.CurrentThread.ManagedThreadId);

            if(_Cmds.ContainsKey(cmdid))
            {
                _Cmds[cmdid].Execute(message, sesson,this);
            }
            else
            {
                LogHelper.Error("无效Cmdkey:{0}", cmdid);
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

                sesson.CloseSession();
            }

        }

        public void AddActor(BattleActor actor)
        {
            _BattleActors.Add(actor.ActorId, actor);
        }

        public void RemoveActor(string actorid)
        {
            _BattleActors.Remove(actorid);
        }

        public BattleActor GetActor(string actorid)
        {
            BattleActor ret = null;
            _BattleActors.TryGetValue(actorid, out ret);

            return ret;
        }

        private Dictionary<string, BattleActor> _BattleActors = new Dictionary<string, BattleActor>();
    }
}
