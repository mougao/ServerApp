using System;
using System.Data;

namespace ShoYoo.Engine.DataBase
{
	/// <summary>
	/// 
	/// </summary>
    public class DBReadItem
	{
		public Action<DataSet> CallBack;
		public string SQL;
		public string GroupName;
	}

	/// <summary>
	/// 
	/// </summary>
    public class DBWriteItem
	{
        public int SequenceID;
        public string SQL;
		public string GroupName;
		public string Key;
	}

	/// <summary>
	/// 
	/// </summary>
	public struct DBCallBackItem
	{
		public string Name;
		public string GroupName;
        public Action<DataSet> CallBack;
		public DataSet Data;
	}
}
