namespace ShoYoo.Engine.Script
{
	using System;
	using System.Collections.Generic;

	public class ScriptDispatcherAdv<TScriptResult>
	{
		private readonly Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TScriptResult>> _Actions
			= new Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TScriptResult>>();

		/// <summary>
		/// 注册无需递归器的脚本处理程序
		/// </summary>
		/// <param name="action_name"></param>
		/// <param name="action_handler"></param>
		public void RegisterHandler(string action_name, Func<ScriptAction, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, (Func<IEnumerable<ScriptAction>, TScriptResult> func, ScriptAction script) => action_handler(script));
		}

		/// <summary>
		/// 注册需不带参数的递归器的脚本处理程序
		/// </summary>
		/// <param name="action_name"></param>
		/// <param name="action_handler"></param>
		public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(ScriptAction action, Func<IEnumerable<ScriptAction>, TScriptResult> invoker)
		{
			if (!_Actions.ContainsKey(action.Action))
				throw new Exception("无法识别的脚本：" + action.ToString());
			else
				return _Actions[action.Action](invoker, action);
		}

		/// <summary>
		/// 执行可完全识别的脚本
		/// </summary>
		/// <param name="action"></param>
		/// <param name="invoker"></param>
		/// <returns></returns>
		/// <remarks>无处理程序时将抛出异常</remarks>
		public TScriptResult TryInvoke(ScriptAction action, Func<IEnumerable<ScriptAction>, TScriptResult> invoker)
		{
			if (!_Actions.ContainsKey(action.Action))
				return default(TScriptResult);
			return _Actions[action.Action](invoker, action);
		}

		/// <summary>
		/// 执行不可完全识别的脚本
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <remarks>无处理程序时将忽略</remarks>
		public bool Contains(string name)
		{
			return _Actions.ContainsKey(name);
		}
	}

	public class ScriptDispatcherAdv<TScriptResult, TScriptArgs> where TScriptArgs : ICloneable
	{
		private readonly Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptArgs,TScriptResult>, ScriptAction, TScriptArgs, TScriptResult>> _Actions
			= new Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptArgs, TScriptResult>, ScriptAction, TScriptArgs, TScriptResult>>();

		/// <summary>
		/// 注册无需递归器的脚本处理程序
		/// </summary>
		/// <param name="action_name"></param>
		/// <param name="action_handler"></param>
		public void RegisterHandler(string action_name, Func<ScriptAction, TScriptArgs, TScriptResult> action_handler)
		{
			_Actions.Add(action_name,
				(Func<IEnumerable<ScriptAction>, TScriptArgs, TScriptResult> func, ScriptAction script, TScriptArgs args) => action_handler(script, args));
		}

        ///// <summary>
        ///// 注册需不带参数的递归器的脚本处理程序
        ///// </summary>
        ///// <param name="action_name"></param>
        ///// <param name="action_handler"></param>
		//public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TScriptArgs, TScriptResult> action_handler)
		//{
		//    _Actions.Add(action_name, action_handler);
		//}

		/// <summary>
		/// 注册可覆盖参数的递归的脚本处理程序
		/// </summary>
		/// <param name="action_name"></param>
		/// <param name="action_handler"></param>
		public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TScriptArgs, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, (Func<IEnumerable<ScriptAction>, TScriptArgs, TScriptResult> func, ScriptAction script, TScriptArgs args) =>
				{
					TScriptArgs args_copy = (TScriptArgs)(args.Clone());
					return action_handler((IEnumerable<ScriptAction> scripts) => func(scripts, args_copy), script, args_copy);
				});
		}

		/// <summary>
		/// 执行可完全识别的脚本
		/// </summary>
		/// <param name="action"></param>
		/// <param name="args"></param>
		/// <param name="invoker"></param>
		/// <returns></returns>
		/// <remarks>无处理程序时将抛出异常</remarks>
		public TScriptResult Invoke(ScriptAction action, TScriptArgs args, Func<IEnumerable<ScriptAction>, TScriptArgs, TScriptResult> invoker)
		{
			if (!_Actions.ContainsKey(action.Action))
				throw new Exception("无法识别的脚本：" + action.ToString());
			else
				return _Actions[action.Action](invoker, action, args);
		}

		/// <summary>
		/// 执行不可完全识别的脚本
		/// </summary>
		/// <param name="action"></param>
		/// <param name="args"></param>
		/// <param name="invoker"></param>
		/// <returns></returns>
		/// <remarks>无处理程序时将忽略</remarks>
		public TScriptResult TryInvoke(ScriptAction action, TScriptArgs args, Func<IEnumerable<ScriptAction>, TScriptArgs, TScriptResult> invoker)
		{
			if (!_Actions.ContainsKey(action.Action))
				return default(TScriptResult);
			return _Actions[action.Action](invoker, action, args);
		}

		public bool Contains(string name)
		{
			return _Actions.ContainsKey(name);
		}
	}

	//public class ScriptDispatcher<TScriptResult, TTarget, TTarget2>
	//{
	//    private readonly Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TTarget, TTarget2, TScriptResult>> _Actions
	//        = new Dictionary<string, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TTarget, TTarget2, TScriptResult>>();

	//    public void RegisterHandler(string action_name, Func<ScriptAction, TTarget, TTarget2, TScriptResult> action_handler)
	//    {
	//        _Actions.Add(action_name, (Func<IEnumerable<ScriptAction>, TScriptResult> func, ScriptAction script, TTarget target, TTarget2 target2) => action_handler(script, target, target2));
	//    }

	//    public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TScriptResult>, ScriptAction, TTarget, TTarget2, TScriptResult> action_handler)
	//    {
	//        _Actions.Add(action_name, action_handler);
	//    }

	//    public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TTarget, TScriptResult>, ScriptAction, TTarget, TScriptResult> action_handler)
	//    {
	//        //TODO 林路 未实现
	//        throw new NotImplementedException();
	//    }

	//    public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TTarget2, TScriptResult>, ScriptAction, TTarget2, TScriptResult> action_handler)
	//    {
	//        //TODO 林路 未实现
	//        throw new NotImplementedException();
	//    }

	//    public void RegisterHandler(string action_name, Func<Func<IEnumerable<ScriptAction>, TTarget, TTarget2, TScriptResult>, ScriptAction, TTarget, TTarget2, TScriptResult> action_handler)
	//    {
	//        //TODO 林路 未实现
	//        throw new NotImplementedException();
	//    }

	//    public TScriptResult Invoke(ScriptAction action, TTarget target, TTarget2 target2, Func<IEnumerable<ScriptAction>, TScriptResult> invoker)
	//    {
	//        if (!_Actions.ContainsKey(action.Action))
	//            throw new Exception("无法识别的脚本：" + action.ToString());
	//        else
	//            return _Actions[action.Action](invoker, action, target, target2);
	//    }

	//    public TScriptResult TryInvoke(ScriptAction action, TTarget target, TTarget2 target2, Func<IEnumerable<ScriptAction>, TScriptResult> invoker)
	//    {
	//        if (!_Actions.ContainsKey(action.Action))
	//            return default(TScriptResult);
	//        return _Actions[action.Action](invoker, action, target, target2);
	//    }

	//    public bool Contains(string name)
	//    {
	//        return _Actions.ContainsKey(name);
	//    }
	//}
}
