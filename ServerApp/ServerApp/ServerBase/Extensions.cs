using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiscUtil.Conversion;

namespace ServerApp
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class Extensions
    {
        static EndianBitConverter BigEndianBitConverter = EndianBitConverter.Big;

        /// <summary>
        /// 读取大端16位无符号整数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Int16 ReadInt16(this byte[] data, int offset)
        {
            return BigEndianBitConverter.ToInt16(data, offset);
        }

        /// <summary>
        /// 读取大端16位无符号整数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static UInt16 ReadUInt16(this byte[] data, int offset)
        {
            return BigEndianBitConverter.ToUInt16(data, offset);
        }
        
        /// <summary>
        /// 读取小端无符号整数
        /// </summary>
        /// <param name="data">二进制数据.</param>
        /// <param name="offset">读取位移.</param>
        /// <returns></returns>
        public static UInt32 ReadUInt32(this byte[] data, int offset)
        {
            return BigEndianBitConverter.ToUInt32(data, offset);
        }

        /// <summary>
        /// 读取小端整数
        /// </summary>
        /// <param name="data">二进制数据.</param>
        /// <param name="offset">读取位移.</param>
        /// <returns></returns>
        public static Int32 ReadInt32(this byte[] data, int offset)
        {
            return BigEndianBitConverter.ToInt32(data, offset);
        }

        public static Int64 ReadInt64(this byte[] data,int offset)
        {
            return BigEndianBitConverter.ToInt64(data, offset);
        }

        /// <summary>
        /// 读取小端无符号长整数
        /// </summary>
        /// <param name="data">二进制数据.</param>
        /// <param name="offset">读取位移.</param>
        /// <returns></returns>
        public static UInt64 ReadUInt64(this byte[] data, int offset)
        {
            return BigEndianBitConverter.ToUInt64(data, offset);
        }
    }
}
