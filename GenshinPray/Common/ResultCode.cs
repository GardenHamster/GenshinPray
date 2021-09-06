using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Common
{
    public class ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static readonly int Success = 0;

        /// <summary>
        /// 错误
        /// </summary>
        public static readonly int Error = 600;

        /// <summary>
        /// 超过api每日调用上限
        /// </summary>
        public static readonly int ApiMaximum = 601;


        


    }
}
