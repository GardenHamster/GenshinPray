using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Util
{
    public static class NumberHelper
    {
        /// <summary>
        /// 计算百分比,保留两位小数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static double GetRate(int num, int sum)
        {
            if (sum == 0) return 0;
            double rate = Convert.ToDouble(num) / sum * 100;
            return Math.Floor(rate * 100) / 100;
        }

    }
}
