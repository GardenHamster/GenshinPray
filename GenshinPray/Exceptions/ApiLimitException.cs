using GenshinPray.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class ApiLimitException : BaseException
    {
        public ApiLimitException(string message) : base(ResultCode.ApiLimit, message)
        {
        }

    }
}
