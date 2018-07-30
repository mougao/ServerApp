using System;
using System.IO;
using XY.MessageEntity;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CMD_LG_CTL_REGIST mss = new CMD_LG_CTL_REGIST();
            mss.account = "rrrrr";

            MemoryStream ms = MessageTransformation.Serialize<CMD_LG_CTL_REGIST>(mss);

            ms.Position = 0;
            CMD_LG_CTL_REGIST mss2 = MessageTransformation.Deserialize<CMD_LG_CTL_REGIST>(ms);


            Console.WriteLine("{0}", mss2.ToString());

            Console.Read();
        }
    }
}
