using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; set; }

        public BaseException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

    }
}
