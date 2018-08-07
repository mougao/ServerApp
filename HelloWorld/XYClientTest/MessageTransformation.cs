using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XYClientTest
{
    public class MessageTransformation
    {
        public static MemoryStream Serialize<T>(T message)
        {
            MemoryStream ret = new MemoryStream();
            Serializer.Serialize<T>(ret, message);

            return ret;
        }

        public static T Deserialize<T>(MemoryStream stream)
        {
            T ret = default(T);

            ret = Serializer.Deserialize<T>(stream);

            return ret;
        }

    }
}
