namespace ShoYoo.Engine.Script
{
	using System;
	using System.Collections.Generic;

	public abstract class BaseScriptsDispatcherAdv<TScriptResult, TActionResult> where TScriptResult : IComparable
	{
        protected BaseScriptsDispatcherAdv()
		{
		}

		protected abstract TScriptResult ResultDefault
		{
			get;
		}

		protected abstract TScriptResult ResultExit
		{
			get;
		}

		protected abstract void ResultMerge(ref TScriptResult origin, TActionResult alteration, ScriptActionPrefix prefix = null);
	}

	public abstract class ScriptsDispatcherAdv<TScriptResult, TActionResult> : BaseScriptsDispatcherAdv<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		protected abstract TActionResult InvokeScript(ScriptAction script, bool ignore_not_exist_exception = false);

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, false), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, true), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}
	}

    public abstract class ScriptsDispatcherAdv<TScriptResult, TActionResult, TTarget> : BaseScriptsDispatcherAdv<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		protected abstract TActionResult InvokeScript(ScriptAction script, TTarget target1, bool ignore_not_exist_exception = false);

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, false), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, true), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}
	}

    public abstract class ScriptsDispatcherAdv<TScriptResult, TActionResult, TTarget, TTarget2> : BaseScriptsDispatcherAdv<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		protected abstract TActionResult InvokeScript(ScriptAction script, TTarget target1, TTarget2 target2, bool ignore_not_exist_exception = false);

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, target2, false), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, target2, true), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}
	}

    public abstract class ScriptsDispatcherAdv<TScriptResult, TActionResult, TTarget, TTarget2, TTarget3> : BaseScriptsDispatcherAdv<TScriptResult, TActionResult>
		where TScriptResult : IComparable
	{
		protected abstract TActionResult InvokeScript(ScriptAction script, TTarget target1, TTarget2 target2, TTarget3 target3, bool ignore_not_exist_exception = false);

		public TScriptResult Invoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2, TTarget3 target3)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, target2, target3, false), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}

		public TScriptResult TryInvoke(IEnumerable<ScriptAction> actions, TTarget target, TTarget2 target2, TTarget3 target3)
		{
			TScriptResult ret = ResultDefault;

			foreach (ScriptAction action in actions)
			{
				ResultMerge(ref ret, InvokeScript(action, target, target2, target3, true), action.Prefix);
				if (ret.CompareTo(ResultExit) == 0)
					break;
			}

			return ret;
		}
	}
}
