using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using XY.MessageEntity;

namespace HelloWorld
{
    class Program
    {
        /// <summary>
        /// 将对象序列化后保存到文件中
        /// </summary>
        public static void SerializeToFile<T>(T obj, string dataFile)
        {
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




        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //CMD_LG_CTL_REGIST mss = new CMD_LG_CTL_REGIST();
            //mss.account = "rrrrr";

            //MemoryStream ms = MessageTransformation.Serialize<CMD_LG_CTL_REGIST>(mss);

            //ms.Position = 0;
            //CMD_LG_CTL_REGIST mss2 = MessageTransformation.Deserialize<CMD_LG_CTL_REGIST>(ms);


            //Console.WriteLine("{0}", mss2.ToString());


            //        Dictionary<string, object> transactionData = new Dictionary<string, object>() {
            //    { "Account","6226221601798333"},
            //    { "Name","张三"},
            //    { "TradeType","存款"},
            //    { "Amount",100.00d}
            //};
            //        SerializeToFile(transactionData, "TD.txt");
            //        var currentData = Deserialize<Dictionary<string, object>>("TD.txt");
            //        foreach (var item in currentData)
            //        {
            //            Console.WriteLine("{0}-{1}", item.Key, item.Value);
            //        }

            //        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            //        logger.Fatal("发生致命错误");
            //        logger.Warn("警告信息");

            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.


            for(int i=0;i<50;i++)
            {
                Console.Write(Guid.NewGuid().GetHashCode().ToString()+"\r\n");

            }



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
