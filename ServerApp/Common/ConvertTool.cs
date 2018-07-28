using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConvertTool
    {
        public static byte[] ConvertIntToByteArray(Int32 m)
        {
            byte[] ret = new byte[4];

            ret[0] = (byte)(m & 0xFF);

            ret[1] = (byte)((m & 0xFF00) >> 8);

            ret[2] = (byte)((m & 0xFF0000) >> 16);

            ret[3] = (byte)((m >> 24) & 0xFF);

            return ret;
        }

        public static int ConvertByteToIntArray(byte[] arry)
        {
            return BitConverter.ToInt32(arry, 0);
        }


    }
}
