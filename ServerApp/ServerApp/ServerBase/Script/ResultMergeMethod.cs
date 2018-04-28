namespace ShoYoo.Engine.Script
{
	public delegate void ResultMergeDelegate<T, U>(ref T origin, U alteration, ScriptActionPrefix prefix = null);
	public delegate void ResultMergeDelegate<T>(ref T origin, T alteration, ScriptActionPrefix prefix = null);
	
	public static class ResultMergeMethod
	{
		public static ResultMergeDelegate<bool, bool> Common =
			(ref bool ret, bool status, ScriptActionPrefix prefix) => { ret = (prefix != null && prefix["!"]) ? !status : status; };
	}
}
