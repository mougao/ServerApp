using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace JDWebBase
{
    public static class RegexString
    {
        private static readonly string _En = @"[^a-zA-Z]";
        private static readonly string _Num = @"[^0-9]";
        private static readonly string _Cha = @"\W";

        /// <summary>
        /// 字符串是否是英文和数字的组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumAndEn(this string input)
        {
            bool ret = System.Text.RegularExpressions.Regex.IsMatch(input, _En) && Regex.IsMatch(input, _Num) && !Regex.IsMatch(input, _Cha);
            return ret;
        }

        private static string sRxStr = @"[\;|\<|\>|\<\=|\>\=|\<\>|\'|\""|\?|\#|\|\*|\\|\,]";
        public static bool HasSQLInject(this string contents)
        {
            if (contents.Length > 0)
            {
                return Regex.IsMatch(contents, sRxStr);
            }
            return false;
        }

    }
}

