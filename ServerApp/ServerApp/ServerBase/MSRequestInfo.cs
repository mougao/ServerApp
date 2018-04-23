using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase.Protocol;

namespace ServerApp
{
    public class MSRequestInfo : IRequestInfo, IRequestInfoInitializer
    {
        public MSRequestInfo()
        {
        }

        /// <summary>
        /// 请求的主键
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 获取请求体内容
        /// </summary>
        /// <value>
        /// 请求体内容
        /// </value>
        public ArraySegment<byte> Data { get; private set; }


        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IRequestEntity Entity { get; private set; }

        void IRequestInfoInitializer.Initialize(string key,ArraySegment<byte> data, IRequestEntity entity)
        {
            Key = key;
            Data = data;
            Entity = entity;
        }
    }
}
