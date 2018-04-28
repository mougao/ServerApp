namespace ShoYoo.Engine.Script
{
	using System;
	using System.Collections.Generic;

	public class BaseScriptsDispatcher<TScriptResult, TActionResult> where TScriptResult : IComparable
	{
		protected readonly ResultMergeDelegate<TScriptResult, TActionResult> _ResultMergingMethod;
		public ResultMergeDelegate<TScriptResult, TActionResult> ResultMerge
		{
			get { return _ResultMergingMethod; }
		}

		protected readonly TScriptResult _ResultDefault;
		public TScriptResult ResultDefault
		{
			get { return _ResultDefault; }
		}

		protected readonly TScriptResult _ResultExit;
		public TScriptResult ResultExit
		{
			get { return _ResultExit; }
		}

		protected BaseScriptsDispatcher(ResultMergeDelegate<TScriptResult, TActionResult> result_merging_method, TScriptResult result_default_value, TScriptResult result_exit_value)
		{
			_ResultMergingMethod = result_merging_method;
			_ResultDefault = result_default_value;
			_ResultExit = result_exit_value;
		}
	}

	public class ScriptsDispatcher<TScriptResult, TActionResult> : BaseScriptsDispatcher<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{

		private readonly Dictionary<string, Func<ScriptAction, TActionResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TActionResult>>();

		public ScriptsDispatcher(
			ResultMergeDelegate<TScriptResult, TActionResult> result_merging_method, TScriptResult result_default_value, TScriptResult result_exit_value)
			: base(result_merging_method, result_default_value, result_exit_value)
		{
		}

		public void RegisterHandler(string action_name, Func<ScriptsDispatcher<TScriptResult, TActionResult>, ScriptAction, TActionResult> action_handler)
		{
			_Actions.Add(action_name, action => action_handler(this, action));
		}

		public void RegisterHandler(string action_name, Func<ScriptAction, TActionResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (!_Actions.ContainsKey(action.Action))
					throw new Exception("无法识别的脚本：" + action.ToString());
				else
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action), action.Prefix);
					if (ret.CompareTo(ResultExit) == 0)
						break;
				}
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (_Actions.ContainsKey(action.Action))
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action), action.Prefix);
					if (ret.CompareTo(ResultExit) == 0)
						break;
				}
			}

			return ret;
		}
	}

	public class ScriptsDispatcher<TScriptResult, TActionResult, TTarget> : BaseScriptsDispatcher<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		private readonly Dictionary<string, Func<ScriptAction, TTarget, TActionResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TTarget, TActionResult>>();

		public ScriptsDispatcher(
			ResultMergeDelegate<TScriptResult, TActionResult> result_merging_method, TScriptResult result_default_value, TScriptResult result_exit_value)
			: base(result_merging_method, result_default_value, result_exit_value)
		{
		}

		public void RegisterHandler(string action_name, Func<ScriptsDispatcher<TScriptResult, TActionResult, TTarget>, ScriptAction, TTarget, TActionResult> action_handler)
		{
			_Actions.Add(action_name, (action, target) => action_handler(this, action, target));
		}

		public void RegisterHandler(string action_name, Func<ScriptAction, TTarget, TActionResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (!_Actions.ContainsKey(action.Action))
					throw new Exception("无法识别的脚本：" + action.ToString());
				else
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (_Actions.ContainsKey(action.Action))
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}
	}

	public class ScriptsDispatcher<TScriptResult, TActionResult, TTarget, TTarget2> : BaseScriptsDispatcher<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		private readonly Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TActionResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TActionResult>>();

		public ScriptsDispatcher(
			ResultMergeDelegate<TScriptResult, TActionResult> result_merging_method, TScriptResult result_default_value, TScriptResult result_exit_value)
			: base(result_merging_method, result_default_value, result_exit_value)
		{
		}

		public void RegisterHandler(string action_name, Func<ScriptsDispatcher<TScriptResult, TActionResult, TTarget, TTarget2>, ScriptAction, TTarget, TTarget2, TActionResult> action_handler)
		{
			_Actions.Add(action_name, (action, target, target2) => action_handler(this, action, target, target2));
		}

		public void RegisterHandler(string action_name, Func<ScriptAction, TTarget, TTarget2, TActionResult> action_handler)
		{
			_Actions.Add(action_name, action_handler);
		}

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (!_Actions.ContainsKey(action.Action))
					throw new Exception("无法识别的脚本：" + action.ToString());
				else
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target, target2), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (_Actions.ContainsKey(action.Action))
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target, target2), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}
	}

	public class ScriptsDispatcher<TScriptResult, TActionResult, TTarget, TTarget2, TTarget3> : BaseScriptsDispatcher<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		private readonly Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TTarget3, TActionResult>> _Actions
			= new Dictionary<string, Func<ScriptAction, TTarget, TTarget2, TTarget3, TActionResult>>();

		public ScriptsDispatcher(
			ResultMergeDelegate<TScriptResult, TActionResult> result_merging_method, TScriptResult result_default_value, TScriptResult result_exit_value)
			: base(result_merging_method, result_default_value, result_exit_value)
		{
		}

		public void RegisterHandler(string action_name, Func<ScriptsDispatcher<TScriptResult, TActionResult, TTarget, TTarget2, TTarget3>, ScriptAction, TTarget, TTarget2, TTarget3, TActionResult> action_handler)
		{
			_Actions.Add(action_name, (action, target, target2, target3) => action_handler(this, action, target, target2, target3));
		}

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2, TTarget3 target3)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (!_Actions.ContainsKey(action.Action))
					throw new Exception("无法识别的脚本：" + action.ToString());
				else
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target, target2, target3), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2, TTarget3 target3)
		{
			TScriptResult ret = _ResultDefault;

			foreach (ScriptAction action in actions)
			{
				if (_Actions.ContainsKey(action.Action))
				{
					_ResultMergingMethod(ref ret, _Actions[action.Action](action, target, target2, target3), action.Prefix);
					if (ret.CompareTo(_ResultExit) == 0)
						break;
				}
			}

			return ret;
		}
	}
}
