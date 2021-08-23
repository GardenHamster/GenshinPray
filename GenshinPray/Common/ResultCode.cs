using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Common
{
    public class ResultCode
    {
        public static readonly int Error = 0;
        public static readonly int Success = 1;

        /// <summary>
        /// 未授权
        /// </summary>
        public static readonly int Unauthorized = 401;

        /// <summary>
        /// 内部异常
        /// </summary>
        public static readonly int ServerError = 500;

    }
}
