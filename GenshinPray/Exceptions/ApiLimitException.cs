using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Exceptions
{
    public class ApiLimitException : Exception
    {
        public ApiLimitException(string message) : base(message)
        {
        }

    }
}
