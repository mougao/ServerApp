namespace JDWebBase
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Data;
	using System.Linq;
	using System.Threading;
	internal enum DataBaseStatus
	{
		Initialized,
		Started,
		Stoped
	}

	public class DataBase
	{
		public static int IdleSleep = 0;

		private string _ConnectionString = null;
		public string ConnectionString
		{
			get { return _ConnectionString; }
		}
		private IDBProvider _Provider = null;
		public IDBProvider Provider
		{
			get
			{
				return _Provider;
			}
		}

		private string _Name;
		public string Name
		{
			get { return _Name; }
		}

		public DataBase(IDBProvider provider, string name,  string connection_string = null)
		{
			_Name = name;
			_Provider = provider;
			_ConnectionString = connection_string;
		}

        private volatile DataBaseStatus Status = DataBaseStatus.Initialized;
		/// <summary>
		/// 连接数据库
		/// </summary>
		/// <param name="connectionstring"></param>
		/// <remarks>连接同时将启动所有异步线程</remarks>
		public void Connect(string connectionstring = null)
		{
			if (connectionstring != null)
				_ConnectionString = connectionstring;

		    if (Status == DataBaseStatus.Stoped)
		    {
		        //MessageBox.Show("数据库已停止");
		        return;
		    }
		    if (Status == DataBaseStatus.Started)
		    {
		        //MessageBox.Show( "已经连接" );
		        return;
		    }

			Status = DataBaseStatus.Started;

		    if (_ConnectionString == null)
		    {
                //MessageBox.Show( "数据库未指定连接字符串" );
		        return;
		    }

			_Provider.Connect(_ConnectionString);
			_Provider.Connect(_ConnectionString);
			
		}

	    public void Close()
	    {
	        _Provider.Close();
	    }
		/// <summary>
		/// 同步写入数据库
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public int Write(string sql)
		{
			if (Status == DataBaseStatus.Stoped)
				throw new Exception(string.Format("{0} 已经关闭", _Name));
			if (Status == DataBaseStatus.Initialized)
				throw new Exception(string.Format("{0} 尚未连接", _Name));

			int res = 0;
		    res = _Provider.Write(sql);
			return res;
		}
        
		/// <summary>
		/// 同步读取数据库
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="callback"></param>
		public void Read(string sql, DBCallBack callback)
		{
			if (Status == DataBaseStatus.Stoped)
				throw new Exception(string.Format("{0} 已经关闭", _Name));
			if (Status == DataBaseStatus.Initialized)
				throw new Exception(string.Format("{0} 尚未连接", _Name));

			DataSet ds = null;
			
			ds = _Provider.Read(sql);

			callback(ds);
		}
	}
}
