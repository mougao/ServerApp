using PaoEntity;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtobufTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CMD_LG_CTL_REGIST mss = new CMD_LG_CTL_REGIST();
            mss.account = "rrrrr";

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize<CMD_LG_CTL_REGIST>(ms, mss);

            ms.Position = 0;
            CMD_LG_CTL_REGIST mss2 = Serializer.Deserialize<CMD_LG_CTL_REGIST>(ms);

            Console.Read();
        }
    }
}
