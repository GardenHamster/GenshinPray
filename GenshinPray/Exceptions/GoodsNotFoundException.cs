using GenshinPray.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class GoodsNotFoundException : BaseException
    {
        public GoodsNotFoundException(string message) : base(ResultCode.GoodsNotFound, message)
        {
        }

    }
}
