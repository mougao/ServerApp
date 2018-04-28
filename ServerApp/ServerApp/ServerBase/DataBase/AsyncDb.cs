namespace ShoYoo.Engine.DataBase
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public enum DbStatus
    {
        /// <summary>
        /// 已配置完成
        /// </summary>
        Initialized,
        /// <summary>
        /// 已开始工作
        /// </summary>
        Running,
        /// <summary>
        /// 已关闭，用于服务器关闭时
        /// </summary>
        Stoped
    }

    public class AsyncDb
    {
        public AsyncDb(string name, string query_connString, string excute_connString, IDbProvider dbprovider, IDbLogger logger, int max_batch_count = 1, bool db_log = true)
        {
            m_Provider = dbprovider;
            m_Name = name;
            m_QueryConnString = query_connString;
            m_ExcuteConnString = excute_connString;
            m_Logger = logger;
            MAX_BATCH_COUNT = max_batch_count;
            m_dblog = db_log;
        }

        #region 字段
        //数据库线程空闲时的休眠时间
        public const int IDLE_SLEEP_MILISECONDS = 10;
        //剩余工作项警告线
        private const int WARNING_REST_COUNT = 10000;
        //最大批处理个数
        private int MAX_BATCH_COUNT = 500;

        private string m_Name;
        protected IDbProvider m_Provider = null;
        protected IDbLogger m_Logger;
        private string m_QueryConnString;
        protected string m_ExcuteConnString;
        protected object m_SyncRoot = new object();
        protected int m_ExcuteSequenceID = 0;
        protected bool m_dblog = true;

        //数据库状态
        private DbStatus m_Status = DbStatus.Initialized;
        //是否正在执行
        protected int m_CallingItemsCount;
        //写入工作队列
        protected ConcurrentQueue<DBWriteItem> m_WritingWorkItems = new ConcurrentQueue<DBWriteItem>();
        //查询工作队列
        private ConcurrentPriorityQueue<DateTime, DBReadItem> m_QueryWorkItems = new ConcurrentPriorityQueue<DateTime, DBReadItem>();
        //回调工作队列
        private static ConcurrentPriorityQueue<DateTime, DBCallBackItem> m_CallBackItems = new ConcurrentPriorityQueue<DateTime, DBCallBackItem>();

        private StreamWriter _LogWriterExt;

        protected StreamWriter m_LogWriterExt
        {
            get
            {
                if (_LogWriterExt == null && m_dblog)
                    _LogWriterExt = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DbLogs" + Path.DirectorySeparatorChar
                                     + m_Name + "_" + DateTime.Now.ToString("yyyy-MM-dd") + "_ext.log", true);
                return _LogWriterExt;
            }
            set
            {
                _LogWriterExt = value;
            }
        }
        private StreamWriter _LogWriter;
        protected StreamWriter m_LogWriter
        {
            get
            {
                if (_LogWriter == null && m_dblog)
                    _LogWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DbLogs" + Path.DirectorySeparatorChar
                                     + m_Name + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", true);
                return _LogWriter;
            }
            set
            {
                _LogWriter = value;
            }
        }
        #endregion

        #region 属性
        public string Name
        {
            get { return m_Name; }
        }

        public IDbProvider Provider
        {
            get { return m_Provider; }
        }

        /// <summary>
        /// 获取AsynDb状态
        /// </summary>
        public DbStatus Status
        {
            get { return m_Status; }
        }

        private int m_QueriedItemsCount = 0;
        /// <summary>
        /// 获取已经执行的查询工作项的个数
        /// </summary>
        public int QueriedItemsCount
        {
            get { return m_QueriedItemsCount; }
        }

        protected int m_ExcutedItemsCount;
        /// <summary>
        /// 获取已经执行的写入工作项的个数
        /// </summary>
        public int ExcutedItemsCount
        {
            get { return m_ExcutedItemsCount; }
        }

        private static int m_CallbackedItemsCount = 0;
        /// <summary>
        /// 获取已经执行的回调工作项的个数
        /// </summary>
        public static int CallbackedItemsCount
        {
            get { return m_CallbackedItemsCount; }
        }
        /// <summary>
        /// 获取剩余查询工作项的个数
        /// </summary>
        public int RestReadItemCount
        {
            get { return m_QueryWorkItems.Count; }
        }
        /// <summary>
        /// 获取剩余写入工作项的个数
        /// </summary>
        public int RestWriteItemCount
        {
            get { return m_WritingWorkItems.Count; }
        }

        /// <summary>
        /// 获取剩余回调工作项
        /// </summary>
        public static int RestCBWorkItemCount
        {
            get { return m_CallBackItems.Count; }
        }
        /// <summary>
        /// 获取所有工作队列是否为空
        /// </summary>
        public bool IsWorkItemEmpty
        {
            get { return RestReadItemCount == 0 && RestWriteItemCount == 0 && m_CallingItemsCount == 0; }
        }
        /// <summary>
        /// 获取回调队列是否为空
        /// </summary>
        public static bool IsCallBackItemEmpty
        {
            get { return m_CallBackItems.Count == 0; }
        }
        #endregion

        #region 方法
        private void QueryProcess()
        {
            //if (m_dblog)
            //{
            //    m_LogWriterExt =
            //        new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DbLogs" + Path.DirectorySeparatorChar
            //                         + m_Name + "_" + DateTime.Now.ToString("yyyy-MM-dd") + "_ext.log", true);
            //}
            Action<DataSet> null_action = (ds) => { };

            m_Logger.Info("{0} thread start", Thread.CurrentThread.Name);
            while (Status == DbStatus.Running)
            {
                Interlocked.Increment(ref m_CallingItemsCount);
                KeyValuePair<DateTime, DBReadItem> item;
                if (m_QueryWorkItems.TryDequeue(out item))
                {
                    Action<DataSet> callback = item.Value.CallBack != null ? item.Value.CallBack : null_action;

                    DataSet ds = null;
                    Performance.Record(
                        item.Value.SQL,
                        item.Value.GroupName,
                        () =>
                        {
                            try
                            {
                                ds = m_Provider.Query(m_QueryConnString, item.Value.SQL);
                            }
                            catch (Exception ex)
                            {
                                m_Logger.Error(item.Value.SQL + Environment.NewLine, ex);
                            }
                        },
                        MonitoringType.DBExcute);
                    m_CallBackItems.Enqueue(item.Key, new DBCallBackItem() { GroupName = item.Value.GroupName, CallBack = callback, Data = ds, Name = item.Value.SQL });

                    Interlocked.Increment(ref m_QueriedItemsCount);
                    Interlocked.Decrement(ref m_CallingItemsCount);
                }
                else
                {
                    Interlocked.Decrement(ref m_CallingItemsCount);
                    Thread.Sleep(IDLE_SLEEP_MILISECONDS);
                }
            }

            if (m_LogWriterExt != null)
            {
                m_LogWriterExt.Close();
                m_LogWriterExt = null;
            }
            Log.InfoFormat("{0} query thread stop", Thread.CurrentThread.Name);
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <remarks>连接同时将启动所有异步线程</remarks>
        public bool Start()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "DbLogs"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "DbLogs");
            }

            if (Status == DbStatus.Stoped)
                throw new Exception("the AnynDb has already stopped .");
            if (Status == DbStatus.Running)
                throw new Exception("the AsynDb has already started .");

            m_Status = DbStatus.Running;

            if (!string.IsNullOrWhiteSpace(m_QueryConnString))
            {
                Thread queryThread = new Thread(QueryProcess);
                queryThread.Name = Name + " Db query";
                queryThread.Start();

                m_Logger.Info("{0} thread started .", queryThread.Name);
            }

            if (!string.IsNullOrWhiteSpace(m_ExcuteConnString))
            {
                Thread excuteThread = new Thread(ExcuteProcess);
                excuteThread.Name = Name + " Db excute";
                excuteThread.Start();

                m_Logger.Info("{0} thread started .", excuteThread.Name);
            }

            return true;
        }

        //基类实现是批量的
        protected virtual void ProcessExecute()
        {
            StringBuilder sbSql = new StringBuilder();
            StringBuilder sbLog = new StringBuilder();
            //int batchCount = Math.Min(m_WritingWorkItems.Count, MAX_BATCH_COUNT);

            int batchCount;
            for (batchCount = 0; batchCount < MAX_BATCH_COUNT; batchCount++)
            {
                DBWriteItem bitem;
                if (!m_WritingWorkItems.TryDequeue(out bitem))
                    break;

                Interlocked.Increment(ref m_CallingItemsCount);
                sbSql.AppendFormat("{0};", bitem.SQL.TrimEnd(';'));
                sbLog.AppendFormat("[{0}] {1};", bitem.SequenceID, bitem.SQL.TrimEnd(';'));
            }

            if (batchCount > 0)
            {
                string sql = sbSql.ToString();
                Performance.Record(
                    "BatchExcute[]",
                    "BatchExcute",
                    () =>
                    {
                        try
                        {
                            m_Provider.Excute(m_ExcuteConnString, sql);
                        }
                        catch (Exception ex)
                        {
                            m_Logger.Error(sbLog.ToString() + Environment.NewLine, ex);
                        }
                    },
                    MonitoringType.DBExcute);
                Interlocked.Add(ref m_ExcutedItemsCount, batchCount);
                Interlocked.Add(ref m_CallingItemsCount, -batchCount);
            }
            else
            {
                Thread.Sleep(IDLE_SLEEP_MILISECONDS);
            }
        }

        private void ExcuteProcess()
        {
            //if (m_dblog)
            //{
            //    m_LogWriter =
            //        new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "DbLogs" + Path.DirectorySeparatorChar
            //                         + m_Name + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", true);
            //}

            m_Logger.Info("{0} thread start", Thread.CurrentThread.Name);
            while (Status == DbStatus.Running)
            {
                ProcessExecute();
            }
            if (m_LogWriter != null)
            {
                m_LogWriter.Close();
                m_LogWriter = null;
            }

            m_Logger.Info("{0} excute thread stop", Thread.CurrentThread.Name);
        }

        /// <summary>
        /// 阻塞式安全停止所选数据库
        /// </summary>
        /// <remarks>本方法检查数据库回调队列及各数据库调用队列。</remarks>
        public static void Stop(IEnumerable<AsyncDb> dbs)
        {
            while (!AsyncDb.IsCallBackItemEmpty || dbs.Count(db => !db.IsWorkItemEmpty) > 0)
            {
                if (!AsyncDb.CallBack())
                {
                    Thread.Sleep(100);
                }
            }
            foreach (AsyncDb db in dbs)
            {
                db.Stop();
            }
        }

        /// <summary>
        /// 阻塞式停止数据库，等待数据库调用队列清空
        /// </summary>
        /// <remarks>本方法不检查数据库回调队列，调用前应确保不再使用本数据库</remarks>
        public void Stop()
        {
            while (!IsWorkItemEmpty)
            {
                Thread.Sleep(100);
            }
            m_Status = DbStatus.Stoped;
        }

        public static bool CallBack()
        {
            KeyValuePair<DateTime, DBCallBackItem> item;
            if (m_CallBackItems.TryDequeue(out item))
            {
                item.Value.CallBack(item.Value.Data);
                Interlocked.Increment(ref m_CallbackedItemsCount);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 同步读取数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="callback"></param>
        public void Query(string sql, Action<DataSet> callback)
        {
            if (Status == DbStatus.Stoped)
                throw new Exception("the AynsDb has stopped.");
            if (Status == DbStatus.Initialized)
                throw new Exception("the AynsDb has't start");

            if (m_dblog)
            {
                m_LogWriterExt.WriteLine("[{0}] [SyncQuery] {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), sql);
                m_LogWriterExt.Flush();
            }

            DataSet ds = null;
            Performance.Record(
                sql, "Sync Db Query", () =>
                {
                    try
                    {
                        ds = m_Provider.Query(m_QueryConnString, sql);
                    }
                    catch (Exception ex)
                    {
                        m_Logger.Error(sql + Environment.NewLine, ex);
                    }
                }, MonitoringType.DBExcute);
            callback(ds);
        }

        /// <summary>
        /// 同步写入数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Excute(string sql)
        {
            if (Status == DbStatus.Stoped)
                throw new Exception("the AynsDb has stopped.");
            if (Status == DbStatus.Initialized)
                throw new Exception("the AynsDb has't start");

            if (m_dblog)
            {
                m_LogWriter.WriteLine("[{0}] [SyncExcute] {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), sql);
                m_LogWriter.Flush();
            }


            int result = 0;
            Performance.Record(
                sql, "Sync Db Excute", () =>
                {
                    try
                    {
                        result = m_Provider.Excute(m_ExcuteConnString, sql);
                    }
                    catch (Exception ex)
                    {
                        m_Logger.Error(sql + Environment.NewLine, ex);
                    }
                }, MonitoringType.DBExcute);
            return result;
        }

		public long InsertAndGetLastInsertedId(string sql)
		{
			return m_Provider.InsertAndGetLastInsertedId(m_QueryConnString, sql);
		}

        /// <summary>
        /// 异步写入数据库 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="group_name"></param>
        /// <param name="key"></param>
        public void AsyncExcute(string sql, string group_name = null, string key = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new Exception("sql can't be empty");
            }
            if (Status == DbStatus.Stoped)
                throw new Exception("the AynsDb has stopped.");
            if (Status == DbStatus.Initialized)
                throw new Exception("the AynsDb has't start");

            lock (m_SyncRoot)
            {
                Interlocked.Increment(ref m_ExcuteSequenceID);
                if (m_dblog)
                {
                    m_LogWriter.WriteLine("[{0}][ID:{1}] {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), m_ExcuteSequenceID, sql);
                    m_LogWriter.Flush();
                }

            }

            m_WritingWorkItems.Enqueue(new DBWriteItem() { SQL = sql, GroupName = group_name, Key = key });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="group_name"></param>
        /// <param name="key"></param>
        public void AsyncExcute(SQLCommandBase sqlcmd, string group_name = null, string key = null)
        {
            string sql = sqlcmd.GetSQL();
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new Exception("sql can't be empty");
            }
            AsyncExcute(sql, group_name ?? sqlcmd.GetGroup(), key);
        }

        /// <summary>
        /// 异步读取数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="callback"></param>
        /// <param name="groupName"></param>
        /// <param name="priority">优先级，时间越早执行优先级越高，默认值为当前系统时间。</param>
        public void AsyncQuery(string sql, Action<DataSet> callback, string groupName, DateTime priority)
        {
            if (Status == DbStatus.Stoped)
                throw new Exception("the AynsDb has stopped.");
            if (Status == DbStatus.Initialized)
                throw new Exception("the AynsDb has't start");

            DateTime p = priority == default(DateTime) ? DateTime.Now : priority;

            if (m_dblog)
            {
                m_LogWriterExt.WriteLine("[{0}] [AsyncQuery] {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), sql);
                m_LogWriterExt.Flush();
            }
            m_QueryWorkItems.Enqueue(p, new DBReadItem() { SQL = sql, CallBack = callback, GroupName = groupName });
        }

        public void AsyncQuery(SQLCommandBase sql, Action<DataSet> callback, string groupName = null, DateTime priority = default(DateTime))
        {
            AsyncQuery(sql.GetSQL(), callback, groupName == null ? sql.GetGroup() : groupName, priority);
        }
        #endregion
    }
}
