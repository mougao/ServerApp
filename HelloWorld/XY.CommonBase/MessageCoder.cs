using System;
using System.Collections.Generic;
using System.Text;

namespace XY.CommonBase
{
    public class MessageCoder
    {
        private static int bufferlength = 2048;
        /// <summary>
        /// 编码
        /// </summary>
        public static byte[] MessageEncoding(byte[] entityData)
        {
            byte[] ret = null;

            if (entityData == null)
                return ret;

            if(entityData.Length+4 > bufferlength)
            {
                LogHelper.Error("发送报文长度太长 length:{0}", entityData.Length);
                return ret;
            }

            int entityLen = entityData.Length;

            ret = new byte[entityLen + 4];
            byte[] header = ConvertHelper.ConvertIntToByteArray(entityLen + 4);

            Array.Copy(header, 0, ret, 0, header.Length);
            Array.Copy(entityData, 0, ret, header.Length, entityLen);

            return ret;
        }


        /// <summary>
        /// 解码
        /// </summary>
        public static List<byte[]> MessageDecoding(ref byte[] buffer, ref int offset)
        {
            List<byte[]> ret = new List<byte[]>();

            if (buffer.Length == 0)
                return ret;

            byte[] header = new byte[4];
            Array.Copy(buffer, 0, header, 0, 4);

            int messagelength = ConvertHelper.ConvertByteToIntArray(header);

            if (offset < messagelength)
                return ret;

            byte[] messagedata = new byte[messagelength - 4];
            Array.Copy(buffer, 4, messagedata, 0, messagelength - 4);

            ret.Add(messagedata);

            for (int i = messagelength; i < offset; i++)
            {
                int index = i - messagelength;

                buffer[index] = buffer[i];
                buffer[i] = 0;
            }

            offset = offset - messagelength;

            List<byte[]> rret = MessageDecoding(ref buffer, ref offset);

            if (rret.Count > 0)
            {
                ret.AddRange(rret);
            }

            return ret;
        }
    }
}
