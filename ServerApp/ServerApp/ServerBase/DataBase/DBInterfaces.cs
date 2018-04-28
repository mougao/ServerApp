namespace ShoYoo.Engine.DataBase
{
    using System;
    using System.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface IDbProvider
    {
        int Excute(string connection_string, string sql);
        DataSet Query(string connection_string, string sql);

		/// <summary>
		/// 插入记录并获取自动增长字段的Id
		/// </summary>
		/// <param name="connString"></param>
		/// <param name="sql"></param>
		/// <returns></returns>
		long InsertAndGetLastInsertedId(string connString, string sql);

        int ConnectionTimeOut
        {
            get;
        }
    }

    public interface IDbLogger
    {
        void Error(string msg, params object[] param);
        void Error(string msg, Exception ex);
        void Info(string msg, params object[] param);
    }
}
