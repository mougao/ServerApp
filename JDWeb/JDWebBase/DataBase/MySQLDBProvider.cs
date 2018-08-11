namespace JDWebBase
{
    using System;
    using System.Collections.Concurrent;
    using System.Data;
    using System.Diagnostics;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public class MySQLProvider : IDBProvider
    {
        //private string _IP;
        //private int _Port;
        //private string _User;
        //private string _Password;
        //private string _Schema;
        private string _ConnectionString = null;

        public MySqlConnection Connection
        {
            get
            {
                return new MySqlConnection( _ConnectionString );
            }
        }

        public MySQLProvider( )
        {
        }

        public MySQLProvider( string connectionstring )
        {
            _ConnectionString = connectionstring;
        }

        public string DatabaseName
        {
            get { return Connection.Database; }
        }

        public void Connect( string ip, int port, string user, string password, string database, string characterset = "utf8" )
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder( );
            sb.UserID = user;
            sb.Password = password;
            sb.Server = ip;
            sb.Port = ( uint )port;
            sb.Pooling = true;
            sb.CharacterSet = characterset;
            sb.Database = database;
            _ConnectionString = sb.ToString( );
        }


        public void Connect( string connectionstring )
        {
            _ConnectionString = connectionstring;
        }

        public DataSet Read( string sql )
        {
            DataSet ds = new DataSet( );
            using ( MySqlDataAdapter da = new MySqlDataAdapter( sql, Connection ) )
            {
                try
                {
                    da.Fill( ds );
                }
                catch ( Exception ex )
                {
                    Log.Info(sql + ex.ToString());
                }
            }

            return ds;
        }

        public int Write( string sql )
        {
            int ret = 0;
            MySqlConnection conn = Connection;
            using ( MySqlCommand command = new MySqlCommand( sql, conn ) )
            {
                conn.Open( );
                try
                {

                    ret = command.ExecuteNonQuery( );
                }
                catch ( Exception ex )
                {
                    Log.Info(sql + ex.ToString());
                }
                conn.Close( );
            }
            return ret;
        }


        public void Close( )
        {
            Connection.Close( );
        }
    }
}
