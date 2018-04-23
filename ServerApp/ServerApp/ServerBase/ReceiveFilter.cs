using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase;
using System.Threading;

namespace ServerApp
{
    /// <summary>
    /// 客户端数据包接受过滤器
    /// </summary>
    public class MSReceiveFilter : FixedHeaderReceiveFilter<MSRequestInfo>, IReceiveFilterInitializer
    {
        public static long AutoSessionHandle = 0;

        public MSReceiveFilter() : base(5) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            //validation number
            byte code = header[offset];
            byte flag = header[offset + 1];         
            //ushort len = header.ReadUInt16(offset + 3);
            //uint cmd = header.ReadUInt32(offset + 5);         
            return (int)header.ReadUInt16(offset + 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="bodyBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected override MSRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            int currentOffset = offset;
            uint key = bodyBuffer.ReadUInt32(currentOffset);
            currentOffset += 4;
            var requestInfo = new MSRequestInfo();
            (requestInfo as IRequestInfoInitializer).Initialize(
                key.ToString(),
                new ArraySegment<byte>(bodyBuffer, currentOffset, length - (currentOffset - offset)), 
                null);
            return requestInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appServer"></param>
        /// <param name="session"></param>
        void IReceiveFilterInitializer.Initialize(IAppServer appServer, IAppSession session)
        {
        }
    }
}
