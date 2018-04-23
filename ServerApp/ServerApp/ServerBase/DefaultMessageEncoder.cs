using MiscUtil.Conversion;
using ShoYoo.Pao.LoginServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class DefaultMessageEncoder : IMessageEncoder
    {
        private const int DefaultBuffSize = 2048;

        public ArraySegment<byte> EncodeMessage(IMessageEntity message)
        {
            return InternalEncodeMessage(message, DefaultBuffSize);
        }

        private ArraySegment<byte> InternalEncodeMessage(IMessageEntity message, int bufferSize)
        {
            var data = new byte[bufferSize];
            var offset = 0;
            data[offset++] = ProtocolUtil.Code;
            data[offset++] = ProtocolUtil.Flag;
            data[offset++] = ProtocolUtil.Version;
            //BODY_LEN
            offset += 2;

            var converter = EndianBitConverter.Big;
       
            var len = message.Serialize(data, offset);
            if (len < 0) //buffer len is not enough, double the length
                return InternalEncodeMessage(message, bufferSize * 2);
            var bodyLength = (ushort)(offset + len - 5);
            converter.CopyBytes(bodyLength, data, 3);

            return new ArraySegment<byte>(data, 0, offset + len);
        }
    }
}
