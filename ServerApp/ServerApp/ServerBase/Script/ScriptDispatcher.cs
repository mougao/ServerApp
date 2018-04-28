namespace ShoYoo.Engine.Script
{
	using System;
	using System.Collections.Generic;

	public class ScriptDispatcher<TScriptResult>
	{
		private readonly Dictionary<string, Func<ScriptAction, TScriptResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TScriptResult>>();

		public void RegisterHandler(string action_name, Func<ScriptAction, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(ScriptAction action)
		{
			if (!_Actions.ContainsKey(action.Action))
				throw new Exception("无法识别的脚本：" + action.ToString());
			else
				return _Actions[action.Action](action);
		}

		public TScriptResult TryInvoke(ScriptAction action)
		{
			if (!_Actions.ContainsKey(action.Action))
				return default(TScriptResult);
			return _Actions[action.Action](action);
		}
	}

	public class ScriptDispatcher<TScriptResult, TTarget>
	{
		private readonly Dictionary<string, Func<ScriptAction, TTarget, TScriptResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TTarget, TScriptResult>>();

		public void RegisterHandler(string action_name, Func<ScriptAction, TTarget, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(ScriptAction action, TTarget target)
		{
			if (!_Actions.ContainsKey(action.Action))
				throw new Exception("无法识别的脚本：" + action.ToString());
			else
				return _Actions[action.Action](action, target);
		}

		public TScriptResult TryInvoke(ScriptAction action, TTarget target)
		{
			if (!_Actions.ContainsKey(action.Action))
				return default(TScriptResult);
			return _Actions[action.Action](action, target);
		}
	}

	public class ScriptDispatcher<TScriptResult, TTarget, TTarget2>
	{
		private readonly Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TScriptResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TScriptResult>>();

		public void RegisterHandler(string action_name, Func<ScriptAction, TTarget, TTarget2, TScriptResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(ScriptAction action, TTarget target, TTarget2 target2)
		{
			if (!_Actions.ContainsKey(action.Action))
				throw new Exception("无法识别的脚本：" + action.ToString());
			else
				return _Actions[action.Action](action, target, target2);
		}

		public TScriptResult TryInvoke(ScriptAction action, TTarget target, TTarget2 target2)
		{
			if (!_Actions.ContainsKey(action.Action))
				return default(TScriptResult);
			return _Actions[action.Action](action, target, target2);
		}
	}
}
