using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using ShoYoo.Engine.Utils;

namespace ShoYoo.Engine.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlProvider : IDbProvider
    {
        /// <summary>
        /// 初始化MySQLProvider的新实例
        /// </summary>
        public MysqlProvider()
        {
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="connection_string">连接字符串</param>
        /// <param name="sql">查询sql</param>
        /// <returns>查询结果集</returns>
        public DataSet Query(string connection_string, string sql)
        {
            DataSet ds = new DataSet();
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, connection_string))
            {
                da.Fill(ds);
            }

            return ds;
        }

        public int Excute(string connection_string, string sql)
        {
            int ret = 0;
            MySqlConnection conn = new MySqlConnection(connection_string);
            //conn.ConnectionTimeout = ConnectionTimeOut;
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                conn.Open();
				command.CommandTimeout = 60000;// conn.ConnectionTimeout;
                ret = command.ExecuteNonQuery();
                conn.Close();
            }
            return ret;
        }

		/// <summary>
		///  插入一条记录, 并获取自动增长字段的值
		/// </summary>
		/// <param name="connString">数据库连接字符串</param>
		/// <param name="sql">要执行的插入语句</param>
		/// <returns>插入记录的自动增长字段Id</returns>
		public long InsertAndGetLastInsertedId(string connString, string sql)
		{
			MySqlConnection conn = new MySqlConnection(connString);
            using (MySqlCommand command = new MySqlCommand(sql, conn))
            {
                conn.Open();
				command.CommandTimeout = 60000;// conn.ConnectionTimeout;
                command.ExecuteNonQuery();
				long ret = command.LastInsertedId;
                conn.Close();

				return ret;
            }
		}

        public int ConnectionTimeOut
        {
            get { 
#if DEBUG
                return 6000;
#else
                return 60;
#endif
            }
        }
    }
}
