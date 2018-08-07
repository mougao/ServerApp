using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace XY.CommonBase
{
    public class DBHelper
    {

        public static string DBPath = ".\\MyDB\\";

        public static void CreateDir()
        {
            string path = DBPath;
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

            dataFile = DBPath + dataFile;

            using (FileStream fileStream = File.Create(dataFile))
            {
                BinaryFormatter binSerializer = new BinaryFormatter();

                try
                {
                    binSerializer.Serialize(fileStream, obj);
                }
                catch (SerializationException e)
                {
                    LogHelper.Error("Failed to serialize. Reason: " + e.Message);
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

            dataFile = DBPath + dataFile;

            using (FileStream fileStream = File.OpenRead(dataFile))
            {
                try
                {
                    BinaryFormatter binDeserializer = new BinaryFormatter();

                    obj = (T)binDeserializer.Deserialize(fileStream);
                }
                catch (SerializationException e)
                {
                    LogHelper.Error("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fileStream.Close();
                }
            }
            return obj;
        }

    }
}
