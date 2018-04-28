using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperServer
{
    public class SuperServerConfig: ConfigurationSection
    {
        /// <summary>
        /// 监听ip地址
        /// </summary>
        [ConfigurationProperty("ip", IsRequired = true)]
        public string IP
        {
            get
            {
                return (string)base["ip"];
            }
            set
            {
                base["ip"] = value;
            }
        }
        
        /// <summary>
        /// 监听端口
        /// </summary>
        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get
            {
                return (int)base["port"];
            }
            set
            {
                base["port"] = value;
            }
        }

        /// <summary>
        /// 网络连接数上限
        /// </summary>
        [ConfigurationProperty("numconnections", IsRequired = true)]
        public int NumConnections
        {
            get
            {
                return (int)base["numconnections"];
            }
            set
            {
                base["numconnections"] = value;
            }
        }

        /// <summary>
        /// 缓存区大小
        /// </summary>
        [ConfigurationProperty("receivebuffersize", IsRequired = true)]
        public int ReceiveBufferSize
        {
            get
            {
                return (int)base["receivebuffersize"];
            }
            set
            {
                base["receivebuffersize"] = value;
            }
        }

    }
}
