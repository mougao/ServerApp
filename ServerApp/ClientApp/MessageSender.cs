using MiscUtil.Conversion;
using PaoEntity;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public interface IMessageEntity
    {
        int Serialize(byte[] data, int offset);
    }

    public class MessageEntity<TProtoBufEntity> : IMessageEntity
    {
        private uint m_MessageID;

        private TProtoBufEntity m_Entity;

        public MessageEntity(uint messageID, TProtoBufEntity entity)
        {
            m_Entity = entity;
            m_MessageID = messageID;
        }

        public int Serialize(byte[] data, int offset)
        {
            var converter = EndianBitConverter.Big;

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize<TProtoBufEntity>(ms, m_Entity);

            byte[] entityData = ms.ToArray();
            int entityLen = entityData.Length;
            //缓冲区大小不足
            if (entityLen + 4 > data.Length - offset)
                return -1;
            converter.CopyBytes(m_MessageID, data, offset);
            Buffer.BlockCopy(entityData, 0, data, offset + 4, entityLen);
            return entityLen + 4;
        }
    }

    public interface IMessageEncoder
    {
        ArraySegment<byte> EncodeMessage(IMessageEntity entity);
    }

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
            data[offset++] = 1;
            data[offset++] = 1;
            data[offset++] = 1;
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

    public class MessageSender
    {
        public void Init(Socket socket)
        {
            M_Socket = socket;
            m_Encoder = new DefaultMessageEncoder();
        }

        public void SendTestMessage()
        {
            CMD_LG_CTL_REGIST rep = new CMD_LG_CTL_REGIST();
            rep.account = "1234324";
            rep.code = "eret";
            rep.psw = "12";

            MessageEntity<CMD_LG_CTL_REGIST> Messageentity = new MessageEntity<CMD_LG_CTL_REGIST>((uint)MsgCode.LG_RG_KEY_REQ, rep);

            ArraySegment<byte> msarray = m_Encoder.EncodeMessage(Messageentity);

            List<ArraySegment<byte>> mslist = new List<ArraySegment<byte>>();
            mslist.Add(msarray);

            M_Socket.Send(mslist,0);
        }

        private Socket M_Socket;
        private IMessageEncoder m_Encoder;

    }
}
