using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取今天开始时间
        /// </summary>
        /// <returns></returns>
        public static DateTime getTodayStart()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day);
        }

        /// <summary>
        /// 获取今天结束时间
        /// </summary>
        /// <returns></returns>
        public static DateTime getTodayEnd()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
        }

    }
}
