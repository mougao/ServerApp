
using Dos.Model;
using Dos.ORM;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using XY.CommonBase;
using XY.MessageEntity;

namespace HelloWorld
{
    
    class Program
    {

        public static void CreateDir()
        {
            string path = ".\\MyDir\\";
            if (Directory.Exists(path))
            {
                Console.WriteLine("此文件夹已经存在，无需创建！");
            }
            else
            {
                Directory.CreateDirectory(path);
                Console.WriteLine(path + " 创建成功!");
            }
        }

        /// <summary>
        /// 将对象序列化后保存到文件中
        /// </summary>
        public static void SerializeToFile<T>(T obj, string dataFile)
        {
            CreateDir();

            dataFile = ".\\MyDir\\" + dataFile;

            using (FileStream fileStream = File.Create(dataFile))
            {
                BinaryFormatter binSerializer = new BinaryFormatter();

                try
                {
                    binSerializer.Serialize(fileStream, obj);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// 从文件中读取数据并反序列化成对象
        /// </summary>
        public static T Deserialize<T>(string dataFile)
        {
            T obj = default(T);

            dataFile = ".\\MyDir\\"+ dataFile;
            using (FileStream fileStream = File.OpenRead(dataFile))
            {
                try
                {
                    BinaryFormatter binDeserializer = new BinaryFormatter();

                    obj = (T)binDeserializer.Deserialize(fileStream);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fileStream.Close();
                }
            }
            return obj;
        }

        public class DBAccount
        {
            public string account;
            public string Password;
            public int Num;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //string connectstring = "server=cdb-fv3v05or.gz.tencentcdb.com;port=10004;uid=root;pwd=%^$%$mou^&gao__12334;database=xy;pooling=true;SslMode=none";

            //DbSession DB = new DbSession(DatabaseType.MySql, connectstring);

            //xyaccount newaccount = new xyaccount();
            //newaccount.account = "testaccount";

            //string straccount = JsonConvert.SerializeObject(newaccount);

            //xyaccount llaccount = JsonConvert.DeserializeObject< xyaccount >(straccount);

            //Console.WriteLine("开始写入 时间：{0}",DateTime.Now.ToUnixTime());

            //for(int i=0;i<100;i++)
            //{
            //    DB.Insert<xyaccount>(newaccount);
            //}

            //Console.WriteLine("结束写入 时间：{0}", DateTime.Now.ToUnixTime());

            //var account = DB.From<xyaccount>().ToList();

            //xyaccount aa = DB.From<xyaccount>().Where(x => x.index == 1).ToFirst();

            //aa.account = "asdsfsdf";

            //DB.Update<xyaccount>(aa);




            Dictionary<string, object> transactionData = new Dictionary<string, object>() {
                { "Account","6226221601798333"},
                { "Name","张三"},
                { "TradeType","存款"},
                { "Amount",100.00d}
            };
            SerializeToFile(transactionData, "TD.txt");
            var currentData = Deserialize<Dictionary<string, object>>("TD.txt");
            foreach (var item in currentData)
            {
                Console.WriteLine("{0}-{1}", item.Key, item.Value);
            }

            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal("发生致命错误");
            logger.Warn("警告信息");

            Console.Read();
        }

        class StatusChecker
        {
            private int invokeCount;
            private int maxCount;

            public StatusChecker(int count)
            {
                invokeCount = 0;
                maxCount = count;
            }

            // This method is called by the timer delegate.
            public void CheckStatus(Object stateInfo)
            {
                AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
                Console.WriteLine("{0} Checking status {1}. Threadid:{2}",
                    DateTime.Now.ToString("h:mm:ss.fff"),
                    (++invokeCount).ToString(),Thread.CurrentThread.ManagedThreadId);

                if (invokeCount == maxCount)
                {
                    // Reset the counter and signal the waiting thread.
                    invokeCount = 0;
                    autoEvent.Set();
                }
            }
        }

    }
}
