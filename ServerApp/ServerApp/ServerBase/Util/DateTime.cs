using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoYoo.Pao.LoginServer
{
	public static class DateTimeExtend
	{
		/// <summary>
		/// DateTime扩展方法，获取当前DateTime实例的Unix时间戳（Int32）。
		/// </summary>
		/// <param name="dte">DateTime实例</param>
		/// <returns>Unix时间戳</returns>
		public static int ToUnixTime(this DateTime dte)
		{
			int intResult = 0;
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			intResult = (int)((dte - startTime).TotalSeconds);
			return intResult;
		}

		/// <summary>
		/// DateTime扩展方法，根据Unix时间戳（Int32）设置当前DateTime实例。
		/// </summary>
		/// <param name="dte">DateTime实例</param>
		/// <param name="unix_time">Unix时间戳</param>
		public static void FromUnixTime(this DateTime dte, int unix_time)
		{
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			dte = startTime.AddSeconds(unix_time);
		}

		/// <summary>
		/// DateTime扩展方法，根据Unix时间戳（Int32）设置当前DateTime实例。
		/// </summary>
		/// <param name="unix_time">Unix时间戳</param>
		public static DateTime FromUnixTime(int unix_time)
		{
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return startTime.AddSeconds(unix_time);
		}

		/// <summary>
		/// DateTime扩展方法，根据周起始时间，判断两个DateTime是否属于同一周。
		/// </summary>
        /// <param name="date">日期一</param>
        /// <param name="targetday">日期二</param>
        /// <param name="begin_time_of_week">周起始时间</param>
		/// <returns>是否属于同一周</returns>
		public static bool InSameWeek(this DateTime date, DateTime targetday, DateTime begin_time_of_week)
		{
			return (int)((date - begin_time_of_week).TotalDays / 7) == (int)((targetday - begin_time_of_week).TotalDays / 7);

		}

		/// <summary>
		/// DateTime扩展方法，根据日起始时间，判断两个DateTime是否属于同一日。
		/// </summary>
        /// <param name="date">日期一</param>
        /// <param name="targetday">日期二</param>
        /// <param name="begin_time_of_day">日起始时间</param>
		/// <returns>是否属于同一日</returns>
		public static bool InSameDay(this DateTime date, DateTime targetday, DateTime begin_time_of_day)
		{
			return (int)((date - begin_time_of_day).TotalDays) == (int)((targetday - begin_time_of_day).TotalDays);

		}

        private static readonly DateTime _default = new DateTime(1970, 1, 1);
		public static DateTime Default
		{
            get { return _default; }
		}

        /// <summary>
        /// 获取日期对应的下周第一天的刷新时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfNextWeek(this DateTime date, DateTime firstDayOfWeek)
        {
            int days = (int)(date - firstDayOfWeek).TotalDays;
            return firstDayOfWeek.AddDays(days + (7 - (days % 7)));
        }

        /// <summary>
        /// 获取日期对应的本周第一天的刷新时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfThisWeek(this DateTime date, DateTime firstDayOfWeek)
        {
            int days = (int)(date - firstDayOfWeek).TotalDays;
            return firstDayOfWeek.AddDays(days - (days % 7));
        }
        public static string ToDBString(this DateTime datetime)
        {
            return datetime.ToString("yyyy/MM/dd HH:mm:ss");
        }
	}
}
