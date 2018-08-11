namespace JDWebBase
{
	using System;
	using System.Data;

	/// <summary>
	/// 
	/// </summary>
	public interface IDBProvider
	{
	    void Connect(string ip, int port, string user, string password, string database, string characterset);
		void Connect(string connectionstring);
        int Write(string sql);
        DataSet Read(string sql);
        string DatabaseName { get; }
	    void Close();
	}
}
