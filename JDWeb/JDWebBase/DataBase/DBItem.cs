namespace JDWebBase
{
	using System.Data;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="ds"></param>
	public delegate void DBCallBack(DataSet ds);

	/// <summary>
	/// 
	/// </summary>
	public class DBReadItem
	{
		public DBCallBack CallBack = null;
		public string SQL = null;
		public string GroupName = null;
	}

	/// <summary>
	/// 
	/// </summary>
	public class DBWriteItem
	{
		public string SQL = null;
		public string GroupName = null;
		public string Key = null;
	}

	/// <summary>
	/// 
	/// </summary>
	public class DBCallBackItem
	{
		public string Name = string.Empty;
		public string GroupName = null;
		public DBCallBack CallBack = null;
		public DataSet Data = null;
	}
}
