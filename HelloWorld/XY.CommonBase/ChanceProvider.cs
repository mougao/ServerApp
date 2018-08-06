using System;
using System.Collections.Generic;
using System.Text;

namespace XY.CommonBase
{
    public class ChanceProvider
    {
        /// <summary>
        /// 概率百分
        /// </summary>
        /// <returns></returns>
        public static bool GetHundrePercent(int min_percent)
        {
            bool ret = false;

            if(min_percent <=0 || min_percent >100)
            {
                return ret;
            }

            Random rd = new Random();

            int point = rd.Next(0, 100);

            if(point>0 && point <= min_percent)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 概率千分
        /// </summary>
        /// <returns></returns>
        public static bool GetThousandPercent(int min_percent)
        {
            bool ret = false;

            if (min_percent <= 0 || min_percent > 1000)
            {
                return ret;
            }

            Random rd = new Random();

            int point = rd.Next(0, 1000);

            if (point > 0 && point <= min_percent)
            {
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 其他概率
        /// </summary>
        /// <returns></returns>
        public static bool GetOtherPercent(int min_percent,int max_percent,int percent)
        {
            bool ret = false;

            if(max_percent < min_percent)
            {
                return ret;
            }

            if (percent <= min_percent || percent > max_percent)
            {
                return ret;
            }

            Random rd = new Random();

            int point = rd.Next(min_percent, max_percent);

            if (point > min_percent && point <= percent)
            {
                ret = true;
            }

            return ret;
        }

    }
}
