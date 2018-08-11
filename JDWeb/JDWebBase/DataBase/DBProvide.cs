using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace JDWebBase
{

    /// <summary>
    /// DBProvide 的摘要说明
    /// </summary>
    public class DBProvide
    {
        /// <summary>
        /// 获取本地数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection GetLocalDBConnection()
        {
            string strconnection = ConfigurationManager.AppSettings["gm_hb"];

            if (string.IsNullOrEmpty(strconnection))
                return null;

            MySqlConnection ret = new MySqlConnection(strconnection);

            return ret;
        }

        /// <summary>
        /// 获取gm hb
        /// </summary>
        /// <returns></returns>
        public static DataBase GetLocalDBBase()
        {

            return GetDBBase("LocalDB", ConfigurationManager.AppSettings["gm_hb"]);
        }

        public static DataBase GetLocalDBBase(string key)
        {

            return GetDBBase(key, ConfigurationManager.AppSettings[key]);
        }

        public static DataBase GetDBBase(string strconnection)
        {
            if (string.IsNullOrEmpty(strconnection))
            {
                return null;
            }
            else
            {
                DataBase ret = new DataBase(new MySQLProvider(), "GM", strconnection);
                ret.Connect();
                return ret;
            }
        }

        public static DataBase GetDBBase(string key, string strconnection)
        {
            if (string.IsNullOrEmpty(strconnection))
            {
                return null;
            }
            else
            {
                DataBase ret = new DataBase(new MySQLProvider(), "GM", strconnection);
                ret.Connect();
                return ret;
            }
        }

        public static bool HasAllSafety(params string[] args)
        {
            bool ret = false;

            foreach (string arg in args)
            {
                if (arg == null)
                {
                    ret = true;

                    break;
                }

                if (arg.HasSQLInject())
                {
                    ret = true;

                    break;
                }
            }


            return ret;
        }

    }
}