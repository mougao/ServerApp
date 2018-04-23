using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShoYoo.Pao.LoginServer.Util
{
    public static class StringHelper
    {
        private static readonly string _En = @"[^a-zA-Z]";
        private static readonly string _Num = @"[^0-9]";
        private static readonly string _Cha = @"\W";
        public static bool IsNumAndEnCh(this string input)
        {
            return Regex.IsMatch(input, _En) && Regex.IsMatch(input, _Num) && !Regex.IsMatch(input, _Cha);

        }       
    }
}
