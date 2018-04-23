using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerApp
{
    public class ProtocolConfig : ConfigurationElement
    {
        [ConfigurationProperty("code",IsRequired = true)]
        public byte Code
        {
            get { return (byte)base["code"]; }
            set { base["code"] = value; }
        }

        [ConfigurationProperty("flag", IsRequired = true)]
        public byte Flag
        {
            get { return (byte)base["flag"]; }
            set { base["flag"] = value; }
        }

        [ConfigurationProperty("version", IsRequired = true)]
        public byte Version
        {
            get { return (byte)base["version"]; }
            set { base["version"] = value; }
        }
    }
}
