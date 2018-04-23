using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoYoo.Pao.LoginServer
{
    static class ProtocolUtil
    {
        private static byte m_Code;
        private static byte m_Flag;
        private static byte m_Version;

        public static void Initialize(byte code, byte flag, byte version)
        {
            m_Code = code;
            m_Flag = flag;
            m_Version = version;
        }
        public static byte Code { get { return m_Code; } }

        public static byte Flag { get { return m_Flag; } }

        public static byte Version { get { return m_Version; } }
    }
}
