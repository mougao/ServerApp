namespace ShoYoo.Engine.Base
{
    using System.Collections.ObjectModel;
    using ShoYoo.Engine.Utils;
    using SuperSocket.SocketBase.Command;
    using System.Collections.Generic;
	using System.Linq;

    /// <summary>
    /// 业务模块基类
    /// </summary>
    public abstract class BaseModule
    {
		private BaseModuleCollection _SubModules = new BaseModuleCollection();

		public BaseModuleCollection SubModules
		{
			get { return _SubModules; }
		}

		List<ICommand> _Commands = new List<ICommand>();
		/// <summary>
		/// 
		/// </summary>
		public List<ICommand> Commands
		{
			get { return _Commands; }
			set { _Commands = value; }
		}

        /// <summary>
        /// 
        /// </summary>
        private bool _Enable;
        
        /// <summary>
        /// 
        /// </summary>
        public bool Enable
        {
            get { return _Enable; }
            set { _Enable = value; }
        }

        /// <summary>
        /// 完成本模块及所有子模块的初始化
        /// </summary>
        public void Initialize()
        {
            RegisterSubModules();
            RegisterEvents();
            RegisterCommands();
        }

        /// <summary>
        /// 只读属性。在派生类中实现时，将返回获取本模块的友好名称，用于运行时跟踪控制。
        /// </summary>
        public abstract string ModuleName
        {
            get;
        }

        /// <summary>
        /// 根据模块的友好名称获取一个直属子模块的引用
        /// </summary>
        /// <param name="modulename">模块名称</param>
        /// <returns>获取的模块，获取失败为null。</returns>
        public BaseModule GetModule(string modulename)
        {
            BaseModule m = null;
            if (_SubModules.Contains(modulename))
                m = _SubModules[modulename];
            return m;
        }

        /// <summary>
        /// 将一个模块注册为本模块的子模块
        /// </summary>
        /// <param name="submodule">被注册的子模块</param>
        protected void RegisterSubModule(BaseModule submodule)
        {
            //Log.Print(string.Format("向 {0} 中注册了 {1}", this.ModuleName, submodule.ModuleName));
            _SubModules.Add(submodule);
        }

        /// <summary>
        /// 将一个命令处理器注册为本模块的命令
        /// </summary>
        /// <param name="command">被注册的命令处理器</param>
        protected void RegisterCommand(ICommand command)
        {
			_Commands.Add(command);
        }

        /// <summary>
        /// 注册本模块的所有子模块
        /// </summary>
        private void RegisterSubModules()
        {
            RegisterMySubModules();

            foreach (BaseModule module in _SubModules)
            {
                module.RegisterSubModules();
            }
        }

        /// <summary>
        /// 注册本模块及所有子模块的事件监听
        /// </summary>
        private void RegisterEvents()
        {
            RegisterMyEvents();
            foreach (BaseModule module in _SubModules)
            {
                module.RegisterEvents();
            }
        }

        /// <summary>
        /// 注册本模块及所有子模块的命令处理器
        /// </summary>
        private void RegisterCommands()
        {
            RegisterMyCommands();
            foreach (BaseModule module in _SubModules)
            {
                module.RegisterCommands();
            }
        }

		public IEnumerable<ICommand> GetInitCommands()
		{
			foreach (ICommand command in _Commands)
				yield return command;

			foreach (BaseModule module in _SubModules)
			{
				foreach (ICommand command in module.GetInitCommands())
				{
					yield return command;
				}
			}
			yield break;
		}

        /// <summary>
        /// 在派生类中实现时，将实现本模块的直属子模块注册。
        /// </summary>
        protected abstract void RegisterMySubModules();

        /// <summary>
        /// 在派生类中实现时，将实现本模块的事件监听注册。
        /// </summary>
        protected abstract void RegisterMyEvents();

        /// <summary>
        /// 在派生类中实现时，将实现本模块的命令处理器注册。
        /// </summary>
        protected abstract void RegisterMyCommands();
    }

    public class BaseModuleCollection : KeyedCollection<string, BaseModule>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override string GetKeyForItem(BaseModule item)
        {
            return item.ModuleName;
        }
    }
}
