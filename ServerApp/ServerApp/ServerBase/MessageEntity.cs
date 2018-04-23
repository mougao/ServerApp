using MiscUtil.Conversion;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public class MessageEntity<TProtoBufEntity> : IMessageEntity
    {
        private uint m_MessageID;

        private TProtoBufEntity m_Entity;

        public MessageEntity(uint messageID,TProtoBufEntity entity)
        {
            m_Entity = entity;
            m_MessageID = messageID;
        }

        public int Serialize(byte[] data,int offset)
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
}
